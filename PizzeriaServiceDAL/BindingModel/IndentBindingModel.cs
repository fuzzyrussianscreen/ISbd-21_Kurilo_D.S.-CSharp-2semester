using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class IndentBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int PizzaId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public int PerformerId { get; set; }
    }
}
