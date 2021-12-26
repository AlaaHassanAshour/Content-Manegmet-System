using CMSystem.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.ViweModel
{
    public class UserViweModel
    {

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; }
    }
}
