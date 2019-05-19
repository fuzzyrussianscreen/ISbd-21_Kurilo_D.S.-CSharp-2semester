using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.Interfaces
{
    public interface ILetterInfoService
    {
        List<LetterInfoViewModel> GetList();
        LetterInfoViewModel GetElement(int id);
        void AddElement(LetterInfoBindingModel model);
    }
}
