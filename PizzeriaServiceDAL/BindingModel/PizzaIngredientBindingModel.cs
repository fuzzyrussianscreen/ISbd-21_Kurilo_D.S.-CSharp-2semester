using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class PizzaIngredientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PizzaId { get; set; }
        [DataMember]
        public int IngredientId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
