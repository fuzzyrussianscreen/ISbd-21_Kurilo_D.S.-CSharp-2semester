using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface IPizzaService
    {
        List<PizzaViewModel> GetList();
        PizzaViewModel GetElement(int id);
        void AddElement(PizzaBindingModel model);
        void UpdElement(PizzaBindingModel model);
        void DelElement(int id);
    }
}
