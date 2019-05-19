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
    [UsageInterface("Интерфейс для работы с письмами")]
    public interface ILetterInfoService
    {
        [UsageMethod("Метод получения списка писем")]
        List<LetterInfoViewModel> GetList();
        [UsageMethod("Метод получения письма по id")]
        LetterInfoViewModel GetElement(int id);
        [UsageMethod("Метод добавления письма")]
        void AddElement(LetterInfoBindingModel model);
    }
}
