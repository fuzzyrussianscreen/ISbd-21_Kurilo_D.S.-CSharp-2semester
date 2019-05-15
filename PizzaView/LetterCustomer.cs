﻿using PizzaView.API;
using PizzeriaServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaView
{
    public static class LetterCustomer
    {
        private static TcpClient letterClient;
        private static SslStream stream;
        private static StreamReader reader;

        private static StreamWriter writer;
        public static void Connect()
        {
            string response = null;
            letterClient = new TcpClient();
            letterClient.Connect("pop.gletter.com", 995);
            stream = new SslStream(letterClient.GetStream());
            stream.AuthenticateAsClient("pop.gletter.com");
            reader = new StreamReader(stream, Encoding.ASCII);
            writer = new StreamWriter(stream);
            response = reader.ReadLine();
            response = SendRequest(reader, writer,
            string.Format("USER {0}", ConfigurationManager.AppSettings["MailLogin"]),
            "Ошибка авторизации, неверный логин");
            response = SendRequest(reader, writer,
            string.Format("PASS {0}",
           ConfigurationManager.AppSettings["MailPassword"]),
            "Ошибка авторизации, неверный пароль");
            CheckMail();
        }
        private static void CheckMail()
        {
            string response = null;
            try
            {
                response = SendRequest(reader, writer,
                string.Format("stat"),
                "Ошибка. Неизвестная команда (stat)");
                string[] numbers = Regex.Split(response, @"\D+");
                int number = Convert.ToInt32(numbers[1]);
                if (number > 0)
                {
                    GetLetters(number);
                }
                response = SendRequest(reader, writer,
                string.Format("Quit"),
                "Ошибка. Неизвестная команда (Quit)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Отправка запроса и полчение ответа от почтового сервера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="message"></param>
        /// /// <param name="errorLetter"></param>
        /// <returns></returns>
        private static string SendRequest(StreamReader reader, StreamWriter writer,
       string message, string errorLetter)
        {
            writer.WriteLine(message);
            writer.Flush();
            var response = reader.ReadLine();
            if (response.StartsWith("-ERR"))
            {
                throw new Exception(errorLetter);
            }
            return response;
        }
        /// <summary>
        /// Получение писем
        /// </summary>
        /// <param name="number"></param>
        private static void GetLetters(int number)
        {
            string response = SendRequest(reader, writer,
            string.Format("RETR {0}", number),
            "Ошибка. Не удалось получить письмо");
            string messageId = string.Empty;
            string from = string.Empty;
            string orderSubjectLetter = string.Empty;
            string orderBodyLetter = string.Empty;
            string date = string.Empty;
            string coding = string.Empty;
            while (true)
            {
                response = reader.ReadLine();
                if (response == ".")
                    break;
                if (response.Length > 4)
                {
                    if (response.StartsWith("From:"))
                    {
                        from = response.Substring(6);
                    }
                    if (response.StartsWith("Date:"))
                    {
                        date = response.Substring(6);
                    }
                    if (response.StartsWith("Letter-ID: "))
                    {
                        messageId = response.Substring(12);
                    }
                    if (response.StartsWith("Subject:"))
                    {
                        orderSubjectLetter = GetSubject(ref response, ref coding);
                        orderBodyLetter = GetBody(response, coding);
                    }
                    if (!string.IsNullOrEmpty(messageId) && !string.IsNullOrEmpty(from)
                    &&
                     !string.IsNullOrEmpty(orderSubjectLetter) &&
                    !string.IsNullOrEmpty(date))
                    {
                        APICustomer.PostRequest<LetterInfoBindingModel,
                       bool>("api/LetterInfo/AddElement",
                       new LetterInfoBindingModel
                       {
                           LetterId = messageId,
                           FromMailAddress = from,
                           DateDelivery = Convert.ToDateTime(date),
                           Subject = orderSubjectLetter,
                           Body = orderBodyLetter
                       });
                        messageId = string.Empty;
                        from = string.Empty;
                        date = string.Empty;
                        orderSubjectLetter = string.Empty;
                        orderBodyLetter = string.Empty;
                    }
                }
            }
        }
        /// <summary>
        /// Получение заголовка сообщения
        /// </summary>
        /// <param name="response"></param>
        /// <param name="coding"></param>
        /// <returns></returns>
        private static string GetSubject(ref string response, ref string coding)
        {
            StringBuilder subject = new StringBuilder(response);
            while (!response.StartsWith("To:"))
            {
                response = reader.ReadLine();
                subject.Append(response);
            }
            MatchCollection rr = Regex.Matches(subject.ToString(),
            @"(?:=\?)([^\?]+)(?:\?B\?)([^\?]*)(?:\?=)");
            if (rr.Count > 0)
            {
                coding = rr[0].Groups[1].Value;
                string message = rr[0].Groups[2].Value;
                byte[] b = Convert.FromBase64String(message);
                return Encoding.GetEncoding(coding).GetString(b);
            }
            else
            {
                return subject.ToString();
            }
        }
        /// <summary>
        /// Получение текста сообщения
        /// </summary>
        /// <param name="response"></param>
        /// <param name="coding"></param>
        /// <returns></returns>
        private static string GetBody(string response, string coding)
        {
            // идем до текста сообщения
            while (!response.StartsWith("Content-Type: text/plain") && response != ".")
            {
                response = reader.ReadLine();
            }
            // считываем следующую строку (там может быть указана кодировка)
            response = reader.ReadLine();
            StringBuilder bodyLetter = new StringBuilder();
            bool needEncoding = false;
            if (response.StartsWith("Content-Transfer-Encoding:"))
            {
                needEncoding = true;
                response = reader.ReadLine();
            }
            while (!response.StartsWith("--"))
            {
                bodyLetter.Append(response);
                response = reader.ReadLine();
            }
            if (needEncoding)
            {
                byte[] b = Convert.FromBase64String(bodyLetter.ToString());
                return Encoding.GetEncoding(coding).GetString(b);
            }
            else
            {
                return bodyLetter.ToString();
            }
        }
    }
}
