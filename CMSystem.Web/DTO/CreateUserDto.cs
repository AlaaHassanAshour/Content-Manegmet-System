using CMSystem.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.DTO
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; }
        public int? OrgnisationId { get; set; }
    }
}
