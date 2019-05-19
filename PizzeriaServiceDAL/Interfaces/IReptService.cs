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
    [UsageInterface("Интерфейс для работы с отчётами")]
    public interface IReptService
    {
        [UsageMethod("Метод добавления отчёта")]
        void SavePizzaPrice(ReptBindingModel model);
        [UsageMethod("Метод получения списка отчётов")]
        List<StoragesLoadViewModel> GetStoragesLoad();
        [UsageMethod("Метод получения отчёта по id")]
        void SaveStoragesLoad(ReptBindingModel model);
        [UsageMethod("Метод получения списка отчётов")]
        List<CustomerIndentViewModel> GetCustomerIndents(ReptBindingModel model);
        [UsageMethod("Метод получения отчёта по id")]
        void SaveCustomerIndents(ReptBindingModel model);
    }
}
