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
    [UsageInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [UsageMethod("Метод получения списка заказов")]
        List<IndentViewModel> GetList();
        [UsageMethod("Метод получения списка свободных заказов")]
        List<IndentViewModel> GetFreeIndents();
        [UsageMethod("Метод добавления заказа")]
        void CreateIndent(IndentBindingModel model);
        [UsageMethod("Метод отправления заказа в работу")]
        void TakeIndentInWork(IndentBindingModel model);
        [UsageMethod("Метод выполнения заказа")]
        void FinishIndent(IndentBindingModel model);
        [UsageMethod("Метод получения оплаты заказа")]
        void PayIndent(IndentBindingModel model);
        [UsageMethod("Метод добавления ингредиентов на склад")]
        void PutIngredientOnStorage(StorageIngredientBindingModel model);
    }
}
