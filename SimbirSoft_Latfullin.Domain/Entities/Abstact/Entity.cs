using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimbirSoft_Latfullin.Domain.Entities.Abstact
{
    public abstract class Entity
    {
        [Key]
        public string Id { get; set; }
    }
}
