using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class CustomerBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Post { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
    }
}
