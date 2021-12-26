using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class Orgnisation:BaseEntity
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public string WorkNature{ get; set; }
        [Required]
        public string Address{ get; set; }
        public string Email{ get; set; }
        public string Telepone{ get; set; }
    }
}
