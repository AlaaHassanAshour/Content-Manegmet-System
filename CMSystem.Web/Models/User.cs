using CMSystem.Web.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Models
{
    public class User:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelete { get; set; }
        public int? OrgnisationId { get; set; }
        public Orgnisation Orgnisation { get; set; }
        public User()
        {
            CreatedAt = DateTime.Now;
            IsDelete = false;
        }
    }
}
