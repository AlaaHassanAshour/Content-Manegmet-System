using CMSystem.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _DB;
        public int? OrganazationId { get; set; }
        public string UserName{ get; set; }
        public BaseController(ApplicationDbContext DB)
        {
            _DB = DB;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            UserName = User.Identity.Name;
            var user = _DB.Users.SingleOrDefault(x => x.UserName == UserName);
            OrganazationId = user.OrgnisationId;
        }
    }
}
