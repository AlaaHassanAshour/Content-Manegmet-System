using CMSystem.Web.Data;
using CMSystem.Web.DTO;
using CMSystem.Web.Models;
using CMSystem.Web.ViweModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Controllers
{

    public class UsersController : BaseController
    {
        private readonly UserManager<User> _userManager;
        public UsersController(ApplicationDbContext DB, UserManager<User> userManager):base(DB)
        {
            
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _DB.Users.Where(x => !x.IsDelete).Select(x => new UserViweModel()
            {
                Id = x.Id,
                FullName = x.FirstName + "" + x.LastName,
                Mobile = x.PhoneNumber,
                Email = x.Email,
                Gender = x.Gender,
                UserType = x.UserType
            }).ToList();


            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations.Where(x => !x.IsDelete), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            if (_DB.Users.Any(x=>x.Email==dto.Email || x.PhoneNumber==dto.Mobile))
            {
                TempData["msg"] = "Dblecet Email or mobile";
                return View();
            }
            var user = new User();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.Mobile;
            user.Gender = dto.Gender;
            user.UserType = dto.UserType;
            user.CreatedAt = DateTime.Now;
            user.UserName = dto.Email;
            user.OrgnisationId = dto.OrgnisationId;

          var result=  await _userManager.CreateAsync(user, "Alaa11$$");
            if (result.Succeeded)
            {
                if (user.UserType==Enums.UserType.Admin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if (user.UserType == Enums.UserType.User)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                }
                else if (user.UserType == Enums.UserType.OrginastionAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "OrganazationAdmin");

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string id)
        {
            var users = _DB.Users.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (users == null)
            {
                return NotFound();
            }
            users.IsDelete = true;
            _DB.Update(users);
            _DB.SaveChanges();
            TempData["msg"] = "Delete Item Successfuly !!  ";
            return RedirectToAction("Index");
        }
    }

}
