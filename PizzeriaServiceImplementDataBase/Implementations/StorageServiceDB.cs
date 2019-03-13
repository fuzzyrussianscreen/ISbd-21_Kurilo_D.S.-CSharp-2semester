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
            List<StorageViewModel> result = context.Storages.Select(rec => new
           StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageIngredients = context.StorageIngredients
            .Where(recPC => recPC.StorageId == rec.Id)
            .Select(recPC => new StorageIngredientViewModel
            {
                Id = recPC.Id,
                StorageId = recPC.StorageId,
                IngredientId = recPC.IngredientId,
                IngredientName = recPC.Ingredient.IngredientName,
                Count = recPC.Count
            })
           .ToList()
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
                    StorageName = element.StorageName,
                    StorageIngredients = context.StorageIngredients
                 .Where(recPC => recPC.StorageId == element.Id)
                 .Select(recPC => new StorageIngredientViewModel
                 {
                     Id = recPC.Id,
                     StorageId = recPC.StorageId,
                     IngredientId = recPC.IngredientId,
                     IngredientName = recPC.Ingredient.IngredientName,
                     Count = recPC.Count
                 })
                 .ToList()

                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StorageBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec =>
                   rec.StorageName == model.StorageName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Storage
                    {
                        StorageName = model.StorageName,
                    };
                    context.Storages.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupIngredients = model.StorageIngredient
                     .GroupBy(rec => rec.IngredientId)
                    .Select(rec => new
                    {
                        IngredientId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupIngredient in groupIngredients)
                    {
                        context.StorageIngredients.Add(new StorageIngredient
                        {
                            StorageId = element.Id,
                            IngredientId = groupIngredient.IngredientId,
                            Count = groupIngredient.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(StorageBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec =>
                   rec.StorageName == model.StorageName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.StorageName = model.StorageName;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.StorageIngredient.Select(rec =>
                   rec.IngredientId).Distinct();
                    var updateIngredients = context.StorageIngredients.Where(rec =>
                   rec.StorageId == model.Id && compIds.Contains(rec.IngredientId));
                    foreach (var updateIngredient in updateIngredients)
                    {
                        updateIngredient.Count =
                       model.StorageIngredient.FirstOrDefault(rec => rec.Id == updateIngredient.Id).Count;
                    }
                    context.SaveChanges();
                    context.StorageIngredients.RemoveRange(context.StorageIngredients.Where(rec =>
                    rec.StorageId == model.Id && !compIds.Contains(rec.IngredientId)));
                    context.SaveChanges();
                    // новые записи
                    var groupIngredients = model.StorageIngredient
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.IngredientId)
                   .Select(rec => new
                   {
                       IngredientId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupIngredient in groupIngredients)
                    {
                        StorageIngredient elementPC =
                       context.StorageIngredients.FirstOrDefault(rec => rec.StorageId == model.Id &&
                       rec.IngredientId == groupIngredient.IngredientId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupIngredient.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.StorageIngredients.Add(new StorageIngredient
                            {
                                StorageId = model.Id,
                                IngredientId = groupIngredient.IngredientId,
                                Count = groupIngredient.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.StorageIngredients.RemoveRange(context.StorageIngredients.Where(rec =>
                        rec.StorageId == id));
                        context.Storages.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
