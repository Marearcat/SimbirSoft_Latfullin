using SimbirSoft_Latfullin.Domain.Entities.Abstact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimbirSoft_Latfullin.Domain.Entities
{
    public class UniqueResult : Entity
    {
        public string Uri { get; set; }
        public string Result { get; set; }
    }
}
