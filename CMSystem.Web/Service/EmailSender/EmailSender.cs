using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CMSystem.Web.Service
{ 
    public class EmailSender : IEmailSender
    {

     //   private readonly IWebHostEnvironment _env;

        //public EmailSender(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}

        public async Task Send(string to, string subject, string body)
        {

            var message = new MailMessage();

            message.To.Add(new MailAddress(to));
            message.From = new MailAddress("alaahashour@gmail.com", "Alaa Hassan Ashour");
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<h1>Test</h1><ul><li>a1</li><li>a2</li><li>a3</li></ul>";

            //var path = Path.Combine(_env.WebRootPath,"logo.pdf");

            //message.Attachments.Add(
            //                 new Attachment(
            //                     new MemoryStream(await File.ReadAllBytesAsync(path)),
            //                     Guid.NewGuid().ToString()
            //  )
            //);


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
