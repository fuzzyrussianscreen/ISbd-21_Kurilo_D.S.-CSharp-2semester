using System;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface IPerformerService
    {
        List<PerformerViewModel> GetList();
        PerformerViewModel GetElement(int id);
        void AddElement(PerformerBindingModel model);
        void UpdElement(PerformerBindingModel model);
        void DelElement(int id);
        PerformerViewModel GetFreeWorker();
    }
}
