using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PizzeriaServiceDAL.ViewModel
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
        [DataMember]
        public string Post { get; set; }
        [DataMember]
        public List<LetterInfoViewModel> Letters { get; set; }
    }
}
