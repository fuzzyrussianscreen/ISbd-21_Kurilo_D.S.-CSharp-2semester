using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class LetterInfo
    {
        public int Id { get; set; }
        public string LetterId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
