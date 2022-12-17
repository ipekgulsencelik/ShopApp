using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ShopApp.WebUI.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email);
        }

        private Task Execute(string subject, string message, string email)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Shop App", "info@shopapp.com"));
            msg.To.Add(new MailboxAddress(subject, email));
            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            { 
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp@gmail.com", 587, false);
                client.Authenticate("celik.ipekgulsen@gmail.com", "Gulsen.1993");

                return client.SendAsync(msg);
            }
        }
    }
}
