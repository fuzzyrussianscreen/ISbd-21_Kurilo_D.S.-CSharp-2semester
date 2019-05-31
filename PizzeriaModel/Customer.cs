﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaModel
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFIO { get; set; }
        [ForeignKey("CustomerId")]
        public virtual List<Indent> Indents { get; set; }
    }
}
