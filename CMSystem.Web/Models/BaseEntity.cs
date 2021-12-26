using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelete{ get; set; }
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDelete = false;
        }
    }
}
