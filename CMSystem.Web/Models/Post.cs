using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class Post:BaseEntity
    {
        [Required]
        public string Title{ get; set; }
        [Required]
        public string Body{ get; set; }
     
        public string ImageUrl{ get; set; }
        public string AttatcmentUrl{ get; set; }
        [Required]
        public DateTime PubilishAt{ get; set; }
        public int PostCategoryId{ get; set; }
        public PostCategory PostCategory { get; set; }
        public int? OrgnisationId { get; set; }
        public Orgnisation Orgnisation { get; set; }
    }
}
