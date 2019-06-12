using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<IndentViewModel> GetList();
        List<IndentViewModel> GetFreeIndents();
        void CreateIndent(IndentBindingModel model);
        void TakeOrderInWork(IndentBindingModel model);
        void FinishOrder(IndentBindingModel model);
        void PayOrder(IndentBindingModel model);
        void PutIngredientOnStorage(StorageIngredientBindingModel model);
    }
}
