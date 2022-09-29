using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;


namespace Common
{
    public class MailSender
    {
        public static void SendEmail(string email, string subject, string message)
        {

            //Sender
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("your email"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message
            };

            //Smtp
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(
                    host: "smtp.gmail.com",
                    port: 587,
                    options: MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("your email", "your app password"); //generate app password from your account
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

        }
    }
}
