using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Mail.Utilities;

namespace TaskFlow.Application.Mail.Interface
{
    public class DotNetSmptEmailAdapter
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(UserInfo.ForEmail, UserInfo.ForName),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(new MailAddress(to));

            using var client = new SmtpClient(UserInfo.SmService)
            {
                Port = 587,
                Credentials = new NetworkCredential(UserInfo.email, UserInfo.app),
                EnableSsl = true,
            };

            await client.SendMailAsync(message);
        }
    }
}
