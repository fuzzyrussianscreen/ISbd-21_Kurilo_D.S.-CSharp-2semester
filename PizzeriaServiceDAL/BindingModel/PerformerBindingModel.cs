using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class PerformerBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PerformerFIO { get; set; }
    }
}
