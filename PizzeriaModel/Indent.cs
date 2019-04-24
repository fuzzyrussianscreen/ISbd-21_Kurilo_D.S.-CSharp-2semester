using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaModel
{
    public class Indent
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? PerformerId { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public IndentStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Performer Performer { get; set; }
    }
}
