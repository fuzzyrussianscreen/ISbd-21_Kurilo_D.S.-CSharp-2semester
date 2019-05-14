using PizzeriaModel;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzeriaServiceImplementDataBase.Implementations
{
    public class LetterInfoServiceDB : ILetterInfoService
    {
        private PizzeriaDbContext context;
        public LetterInfoServiceDB(PizzeriaDbContext context)
        {
            this.context = context;
        }
        public List<LetterInfoViewModel> GetList()
        {
            List<LetterInfoViewModel> result = context.LetterInfos
            .Where(rec => !rec.CustomerId.HasValue)
            .Select(rec => new LetterInfoViewModel
            {
                LetterId = rec.LetterId,
                CustomerName = rec.FromMailAddress,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
            .ToList();
            return result;
        }
        public LetterInfoViewModel GetElement(int id)
        {
            LetterInfo element = context.LetterInfos.Include(rec => rec.Customer).FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new LetterInfoViewModel
                {
                    LetterId = element.LetterId,
                    CustomerName = element.Customer.CustomerFIO,
                    DateDelivery = element.DateDelivery,
                    Subject = element.Subject,
                    Body = element.Body
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(LetterInfoBindingModel model)
        {
            LetterInfo element = context.LetterInfos.FirstOrDefault(rec =>
           rec.LetterId == model.LetterId);
            if (element != null)
            {
                return;
            }
            var message = new LetterInfo
            {
                LetterId = model.LetterId,
                FromMailAddress = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };
            var mailAddress = Regex.Match(model.FromMailAddress,
           @"(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9az])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))");
            if (mailAddress.Success)
            {
                var client = context.Customers.FirstOrDefault(rec => rec.Post ==
               mailAddress.Value);
                if (client != null)
                {
                    message.CustomerId = client.Id;
                }
            }
            context.LetterInfos.Add(message);
            context.SaveChanges();
        }
    }
}
