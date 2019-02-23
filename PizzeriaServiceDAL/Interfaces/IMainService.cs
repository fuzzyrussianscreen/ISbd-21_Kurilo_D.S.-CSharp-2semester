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
        void CreateIndent(IndentBindingModel model);
        void TakeIndentInWork(IndentBindingModel model);
        void FinishIndent(IndentBindingModel model);
        void PayIndent(IndentBindingModel model);
        void PutComponentOnStorage(StorageIngredientBindingModel model);
    }
}
