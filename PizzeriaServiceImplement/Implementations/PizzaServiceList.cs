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
    public class PizzaServiceList : IPizzaService
    {
        private DataListSingleton source;
        public PizzaServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PizzaViewModel> GetList()
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и ихколичество
                List<PizzaIngredientViewModel> pizzaIngredients = new
    List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredient.Count; ++j)
                {
                    if (source.PizzaIngredient[j].PizzaId == source.Pizzas[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredient[j].PizzaId ==
                           source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        pizzaIngredients.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredient[j].Id,
                            PizzaId = source.PizzaIngredient[j].PizzaId,
                            IngredientId = source.PizzaIngredient[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.PizzaIngredient[j].Count
                        });
                    }
                }
                result.Add(new PizzaViewModel
                {
                    Id = source.Pizzas[i].Id,
                    PizzaName = source.Pizzas[i].PizzaName,
                    Price = source.Pizzas[i].Price,
                    PizzaIngredients = pizzaIngredients
                });
            }
            return result;
        }
        public PizzaViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<PizzaIngredientViewModel> pizzaIngredients = new
    List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredient.Count; ++j)
                {
                    if (source.PizzaIngredient[j].PizzaId == source.Pizzas[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredient[j].IngredientId ==
                           source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        pizzaIngredients.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredient[j].Id,
                            PizzaId = source.PizzaIngredient[j].PizzaId,
                            IngredientId = source.PizzaIngredient[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.PizzaIngredient[j].Count
                        });
                    }
                }
                if (source.Pizzas[i].Id == id)
                {
                    return new PizzaViewModel
                    {
                        Id = source.Pizzas[i].Id,
                        PizzaName = source.Pizzas[i].PizzaName,
                        Price = source.Pizzas[i].Price,
                        PizzaIngredients = pizzaIngredients
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(PizzaBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id > maxId)
                {
                    maxId = source.Pizzas[i].Id;
                }
                if (source.Pizzas[i].PizzaName == model.PizzaName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Pizzas.Add(new Pizza
            {
                Id = maxId + 1,
                PizzaName = model.PizzaName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredient.Count; ++i)
            {
                if (source.PizzaIngredient[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredient[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.PizzaIngredient.Count; ++i)
            {
                for (int j = 1; j < model.PizzaIngredient.Count; ++j)
                {
                    if (model.PizzaIngredient[i].IngredientId ==
                    model.PizzaIngredient[j].IngredientId)
                    {
                        model.PizzaIngredient[i].Count +=
                        model.PizzaIngredient[j].Count;
                        model.PizzaIngredient.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.PizzaIngredient.Count; ++i)
            {
                source.PizzaIngredient.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = maxId + 1,
                    IngredientId = model.PizzaIngredient[i].IngredientId,
                    Count = model.PizzaIngredient[i].Count
                });
            }
        }
        public void UpdElement(PizzaBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Pizzas[i].PizzaName == model.PizzaName &&
                source.Pizzas[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Pizzas[index].PizzaName = model.PizzaName;
            source.Pizzas[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredient.Count; ++i)
            {
                if (source.PizzaIngredient[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredient[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.PizzaIngredient.Count; ++i)
            {
                if (source.PizzaIngredient[i].PizzaId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.PizzaIngredient.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.PizzaIngredient[i].Id ==
                       model.PizzaIngredient[j].Id)
                        {
                            source.PizzaIngredient[i].Count =
                           model.PizzaIngredient[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.PizzaIngredient.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.PizzaIngredient.Count; ++i)
            {
                if (model.PizzaIngredient[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.PizzaIngredient.Count; ++j)
                    {
                        if (source.PizzaIngredient[j].PizzaId == model.Id &&
                        source.PizzaIngredient[j].IngredientId ==
                       model.PizzaIngredient[i].IngredientId)
                        {
                            source.PizzaIngredient[j].Count +=
                           model.PizzaIngredient[i].Count;
                            model.PizzaIngredient[i].Id =
                           source.PizzaIngredient[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.PizzaIngredient[i].Id == 0)
                    {
                        source.PizzaIngredient.Add(new PizzaIngredient
                        {
                            Id = ++maxPCId,
                            PizzaId = model.Id,
                            IngredientId = model.PizzaIngredient[i].IngredientId,
                            Count = model.PizzaIngredient[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.PizzaIngredient.Count; ++i)
            {
                if (source.PizzaIngredient[i].PizzaId == id)
                {
                    source.PizzaIngredient.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}

