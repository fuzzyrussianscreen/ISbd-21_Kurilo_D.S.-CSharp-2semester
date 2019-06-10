using PizzeriaServiceDAL;
using PizzeriaServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class PizzaBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PizzaName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<PizzaIngredientBindingModel> PizzaIngredient { get; set; }
    }
}
