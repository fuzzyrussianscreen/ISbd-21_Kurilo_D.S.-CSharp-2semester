using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.ViewModel
{
    public class CustomerIndentViewModel
    {
        public string CustomerName { get; set; }
        public string DateCreate { get; set; }
        public string PizzaName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
