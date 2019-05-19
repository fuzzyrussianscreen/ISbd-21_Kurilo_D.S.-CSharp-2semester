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
    [UsageInterface("Интерфейс для работы с работниками")]
    public interface IPerformerService
    {
        [UsageMethod("Метод получения списка работников")]
        List<PerformerViewModel> GetList();
        [UsageMethod("Метод получения работника по id")]
        PerformerViewModel GetElement(int id);
        [UsageMethod("Метод добавления работника")]
        void AddElement(PerformerBindingModel model);
        [UsageMethod("Метод изменения данных по работнику")]
        void UpdElement(PerformerBindingModel model);
        [UsageMethod("Метод удаления работника")]
        void DelElement(int id);
        [UsageMethod("Метод удаления работника")]
        PerformerViewModel GetFreeWorker();
    }
}
