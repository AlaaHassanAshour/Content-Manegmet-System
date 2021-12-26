using CMSystem.Web.Data;
using CMSystem.Web.Models;
using CMSystem.Web.Service;
using CMSystem.Web.ViweModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CMSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEmailSender _emailSender;
        public string bodys { get; set; }
        public HomeController(IEmailSender emailSender, ApplicationDbContext DB) : base(DB)
        {
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            string UserName = User.Identity.Name;
            var user = _DB.Users.SingleOrDefault(x => x.UserName == UserName);
            string users = user.Email;
            bodys = user.FirstName;

            await Send("alaa199903@gmail.com", users, "welcome !!");
            var dasbord = new DashbordViweModel();
            dasbord.UserCount = _DB.Users.Count(x => !x.IsDelete);
            dasbord.OrgCount = _DB.Orgnisations.Count(x => !x.IsDelete);
            dasbord.PostsCount = _DB.Posts.Count(x => !x.IsDelete);
            dasbord.EvantCount = _DB.Events.Count(x => !x.IsDelete);
            return View(dasbord);
        }
        public async Task Send(string to, string subject, string body)
        {

            var message = new MailMessage();

            message.To.Add(new MailAddress(to));
            message.From = new MailAddress("alaahashour@gmail.com", "Alaa Hassan Ashour");
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = bodys;
            using var emailClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alaahashour@gmail.com", "19alaa0399")
            };

            await emailClient.SendMailAsync(message);
        }
    }
}
