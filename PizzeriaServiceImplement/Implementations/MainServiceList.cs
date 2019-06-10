using PizzeriaModel;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceImplement.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = source.Indents
                .Select(rec => new IndentViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    PizzaId = rec.PizzaId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id ==
                    rec.CustomerId)?.CustomerFIO,
                    PizzaName = source.Pizzas.FirstOrDefault(recP => recP.Id ==
                    rec.PizzaId)?.PizzaName,
                })
                .ToList();

            return result;
        }
        public void CreateIndent(IndentBindingModel model)
        {
            int maxId = source.Indents.Count > 0 ? source.Indents.Max(rec => rec.Id) : 0;
            source.Indents.Add(new Indent
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                PizzaId = model.PizzaId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = IndentStatus.Принят
            });
        }
        public void TakeOrderInWork(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != IndentStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            var pizzaIngredients = source.PizzaIngredient.Where(rec => rec.PizzaId
           == element.PizzaId);
            foreach (var pizzaIngredient in pizzaIngredients)
            {
                int countOnStorages = source.StorageIngredients
                .Where(rec => rec.IngredientId ==
               pizzaIngredient.IngredientId)
                .Sum(rec => rec.Count);
                if (countOnStorages < pizzaIngredient.Count * element.Count)
                {
                    var componentName = source.Ingredients.FirstOrDefault(rec => rec.Id ==
                   pizzaIngredient.IngredientId);
                    throw new Exception("Не достаточно компонента " +
                   componentName?.IngredientName + " требуется " + (pizzaIngredient.Count * element.Count) +
                   ", в наличии " + countOnStorages);
                }
            }

            // списываем
            foreach (var pizzaIngredient in pizzaIngredients)
            {
                int countOnStorages = pizzaIngredient.Count * element.Count;
                var stockIngredients = source.StorageIngredients.Where(rec => rec.IngredientId
               == pizzaIngredient.IngredientId);
                foreach (var stockIngredient in stockIngredients)
                {
                    // компонентов на одном слкаде может не хватать
                    if (stockIngredient.Count >= countOnStorages)
                    {
                        stockIngredient.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= stockIngredient.Count;
                        stockIngredient.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = IndentStatus.Выполняется;
        }
        public void FinishOrder(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != IndentStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = IndentStatus.Готов;
        }
        public void PayOrder(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != IndentStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = IndentStatus.Оплачен;
        }

        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            StorageIngredient element = source.StorageIngredients.FirstOrDefault(rec =>
           rec.StorageId == model.StorageId && rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageIngredients.Count > 0 ?
               source.StorageIngredients.Max(rec => rec.Id) : 0;
                source.StorageIngredients.Add(new StorageIngredient
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
        }
        
        public List<IndentViewModel> GetFreeIndents()
        {
            List<IndentViewModel> result = source.Indents
            .Where(x => x.Status == IndentStatus.Принят || x.Status ==
           IndentStatus.НедостаточноРесурсов)
            .Select(rec => new IndentViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
    }
}
