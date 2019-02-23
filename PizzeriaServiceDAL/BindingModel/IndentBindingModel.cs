using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaServiceDAL.BindingModel
{
    public class IndentBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
