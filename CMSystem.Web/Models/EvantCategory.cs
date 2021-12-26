using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class EvantCategory:BaseEntity
    {
        [Required]
        public string Name{ get; set; }
        public List<Event> Events{ get; set; }
    }
}
