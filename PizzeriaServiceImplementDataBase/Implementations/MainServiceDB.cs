using PizzeriaModel;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace PizzeriaServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private PizzeriaDbContext context;
        public MainServiceDB(PizzeriaDbContext context)
        {
            this.context = context;
        }
        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = context.Indents.Select(rec => new IndentViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                PizzaId = rec.PizzaId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerFIO = rec.Customer.CustomerFIO,
                PizzaName = rec.Pizza.PizzaName,
                PerformerId = rec.Performer.Id,
                PerformerName = rec.Performer.PerformerFIO
            })
            .ToList();
            return result;
        }
        public void CreateIndent(IndentBindingModel model)
        {
            context.Indents.Add(new Indent
            {
                CustomerId = model.CustomerId,
                PizzaId = model.PizzaId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = IndentStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeIndentInWork(IndentBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != IndentStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var pizzaIngredients = context.PizzaIngredients.Include(rec => rec.Ingredient).Where(rec => rec.PizzaId == element.PizzaId);
                    // списываем
                    foreach (var pizzaIngredient in pizzaIngredients)
                    {
                        int countOnStorages = pizzaIngredient.Count * element.Count;
                        var storageIngredients = context.StorageIngredients.Where(rec => rec.IngredientId == pizzaIngredient.IngredientId);
                        foreach (var storageIngredient in storageIngredients)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (storageIngredient.Count >= countOnStorages)
                            {
                                storageIngredient.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= storageIngredient.Count;
                                storageIngredient.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                           pizzaIngredient.Ingredient.IngredientName + " требуется " + pizzaIngredient.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.PerformerId = model.PerformerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = IndentStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != IndentStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = IndentStatus.Готов;
            context.SaveChanges();
        }

        public void PayIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != IndentStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = IndentStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            StorageIngredient element = context.StorageIngredients.FirstOrDefault(rec =>
           rec.StorageId == model.StorageId && rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StorageIngredients.Add(new StorageIngredient
                {
                    StorageId = model.StorageId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }

        public List<IndentViewModel> GetFreeIndents()
        {
            List<IndentViewModel> result = context.Indents
            .Where(x => x.Status == IndentStatus.Принят || x.Status ==
           IndentStatus.НедостаточноРесурсов)
            .Select(rec => new IndentViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
               MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
