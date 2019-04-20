﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class Performer
    {
        public int Id { get; set; }
        [Required]
        public string PerformerFIO { get; set; }
        [ForeignKey("PerformerId")]
        public virtual List<Indent> Indents { get; set; }
    }
}
