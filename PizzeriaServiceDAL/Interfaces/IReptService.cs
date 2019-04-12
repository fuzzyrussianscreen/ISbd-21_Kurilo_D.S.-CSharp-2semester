using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface IReptService
    {
        void SavePizzaPrice(ReptBindingModel model);
        List<StoragesLoadViewModel> GetStoragesLoad();
        void SaveStoragesLoad(ReptBindingModel model);
        List<CustomerIndentViewModel> GetCustomerIndents(ReptBindingModel model);
        void SaveCustomerIndents(ReptBindingModel model);
    }
}
