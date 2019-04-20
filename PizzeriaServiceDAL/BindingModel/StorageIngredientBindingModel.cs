﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class StorageIngredientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int IngredientId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
