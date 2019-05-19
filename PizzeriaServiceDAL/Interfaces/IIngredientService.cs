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
    [UsageInterface("Интерфейс для работы с ингредиентами")]
    public interface IIngredientService
    {
        [UsageMethod("Метод получения списка ингредиентов")]
        List<IngredientViewModel> GetList();
        [UsageMethod("Метод получения ингредиента по id")]
        IngredientViewModel GetElement(int id);
        [UsageMethod("Метод добавления ингредиента")]
        void AddElement(IngredientBindingModel model);
        [UsageMethod("Метод изменения данных по ингредиенту")]
        void UpdElement(IngredientBindingModel model);
        [UsageMethod("Метод удаления ингредиента")]
        void DelElement(int id);
    }
}
