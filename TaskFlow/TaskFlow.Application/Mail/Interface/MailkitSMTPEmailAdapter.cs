using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Mail.Utilities;

namespace TaskFlow.Application.Mail.Interface
{
    public class MailkitSMTPEmailAdapter : IEmailService
    {

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(UserInfo.SmService, 465, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(UserInfo.email, UserInfo.app);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(UserInfo.ForName, UserInfo.ForEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}
