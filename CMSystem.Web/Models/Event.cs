using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class Event:BaseEntity
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Description{ get; set; }
        public DateTime? EvantDate{ get; set; }
        public int EvantCategoryId { get; set; }
        public EvantCategory EvantCategory { get; set; }
        public List<EvantImage> EvantImages{ get; set; }
        public int? OrgnisationId { get; set; }
        public Orgnisation Orgnisation { get; set; }
    }
}
