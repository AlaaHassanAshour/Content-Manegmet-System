using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> InitRole()
        {
          await  _roleManager.CreateAsync(new IdentityRole("Admin"));
          await  _roleManager.CreateAsync(new IdentityRole("User"));
          await  _roleManager.CreateAsync(new IdentityRole("OrganazationAdmin"));
            return Ok("Done");
        }
    }
}
