﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class IngredientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IngredientName { get; set; }
    }
}
