using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface IIngredientService
    {
        List<IngredientViewModel> GetList();
        IngredientViewModel GetElement(int id);
        void AddElement(IngredientBindingModel model);
        void UpdElement(IngredientBindingModel model);
        void DelElement(int id);
    }
}
