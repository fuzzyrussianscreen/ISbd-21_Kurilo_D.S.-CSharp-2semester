using PizzeriaModel;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceImplementDataBase.Implementations
{
    public class PerformerServiceDB : IPerformerService
    {
        private PizzeriaDbContext context;
        public PerformerServiceDB(PizzeriaDbContext context)
        {
            this.context = context;
        }
        public List<PerformerViewModel> GetList()
        {
            List<PerformerViewModel> result = context.Performers
            .Select(rec => new PerformerViewModel
            {
                Id = rec.Id,
                PerformerFIO = rec.PerformerFIO
            })
            .ToList();
            return result;
        }
        public PerformerViewModel GetElement(int id)
        {
            Performer element = context.Performers.FirstOrDefault(rec => rec.Id ==
           id);
            if (element != null)
            {
                return new PerformerViewModel
                {
                    Id = element.Id,
                    PerformerFIO = element.PerformerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PerformerBindingModel model)
        {
            Performer element = context.Performers.FirstOrDefault(rec =>
           rec.PerformerFIO == model.PerformerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Performers.Add(new Performer
            {
                PerformerFIO = model.PerformerFIO
            });
            context.SaveChanges();
        }
        public void UpdElement(PerformerBindingModel model)
        {
            Performer element = context.Performers.FirstOrDefault(rec =>
            rec.PerformerFIO == model.PerformerFIO &&
           rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Performers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.PerformerFIO = model.PerformerFIO;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Performer element = context.Performers.FirstOrDefault(rec => rec.Id ==
           id);
            if (element != null)
            {
                context.Performers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public PerformerViewModel GetFreeWorker()
        {
            var indentsWorker = context.Performers
            .Select(x => new
            {
                ImplId = x.Id,
                Count = context.Indents.Where(o => o.Status == IndentStatus.Выполняется && o.PerformerId == x.Id).Count()
            })
            .OrderBy(x => x.Count)
            .FirstOrDefault();
            if (indentsWorker != null)
            {
                return GetElement(indentsWorker.ImplId);
            }
            return null;
        }
    }
}
