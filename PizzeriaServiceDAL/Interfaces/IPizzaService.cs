using PizzeriaServiceDAL.Attributies;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    [UsageInterface("Интерфейс для работы с пиццами")]
    public interface IPizzaService
    {
        [UsageMethod("Метод получения списка пицц")]
        List<PizzaViewModel> GetList();
        [UsageMethod("Метод получения пицц по id")]
        PizzaViewModel GetElement(int id);
        [UsageMethod("Метод добавления пицц")]
        void AddElement(PizzaBindingModel model);
        [UsageMethod("Метод изменения данных по пицце")]
        void UpdElement(PizzaBindingModel model);
        [UsageMethod("Метод удаления пицц")]
        void DelElement(int id);
    }
}
