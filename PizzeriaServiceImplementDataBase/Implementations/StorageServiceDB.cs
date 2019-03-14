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
    public class StorageServiceDB : IStorageService
    {
        private PizzeriaDbContext context;
        public StorageServiceDB(PizzeriaDbContext context)
        {
            this.context = context;
        }
        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = context.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName
            })
            .ToList();
            return result;
        }
        public StorageViewModel GetElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Storages.Add(new Storage
            {
                StorageName = model.StorageName
            });
            context.SaveChanges();
        }
        public void UpdElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Storages.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
