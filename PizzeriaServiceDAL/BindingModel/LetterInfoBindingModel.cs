using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceDAL.BindingModel
{
    [DataContract]
    public class LetterInfoBindingModel
    {
        [DataMember]
        public string LetterId { get; set; }
        [DataMember]
        public string FromMailAddress { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public DateTime DateDelivery { get; set; }
    }
}
