using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaServiceDAL.ViewModel
{
    public class IndentViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFIO { get; set; }
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
        public string DateCreate { get; set; }
        public string DateImplement { get; set; }
        public int? PerformerId { get; set; }
        public string PerformerName { get; set; }
    }
}
