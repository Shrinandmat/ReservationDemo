using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ReservationDemo.DTOs;
using System.Configuration;
using System.Net;
using System.Net.Mail;


namespace ReservationDemo.Controllers
{

    //public void SendEmail(EmailDto request)
    //{
    //    var email = new MimeMessage();

    //    // Get settings from Web.config
    //    email.From.Add(MailboxAddress.Parse(ConfigurationManager.AppSettings["EmailUsername"]));
    //    email.To.Add(MailboxAddress.Parse(request.To));

    //    email.Subject = request.Subject;
    //    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

    //    var smtp = new System.Net.Mail.SmtpClient();
    //    smtp.Connect(
    //        ConfigurationManager.AppSettings["EmailHost"],
    //        int.Parse(ConfigurationManager.AppSettings["EmailPort"] ?? "587"),
    //        MailKit.Security.SecureSocketOptions.StartTls);

    //    smtp.Authenticate(
    //        ConfigurationManager.AppSettings["EmailUsername"],
    //        ConfigurationManager.AppSettings["EmailPassword"]);

    //    smtp.Send(email);
    //    smtp.Disconnect(true);
    //}

    //public void SendEmail(EmailDto request)
    //{
    //    var email = new MimeMessage();

    //    email.From.Add(MailboxAddress.Parse(ConfigurationManager.AppSettings["EmailUsername"]));
    //    email.To.Add(MailboxAddress.Parse(request.To));

    //    email.Subject = request.Subject;
    //    email.Body = new TextPart("html") { Text = request.Body };

    //    using (var smtp = new System.Net.Mail.SmtpClient())
    //    {
    //        smtp.Connect(
    //            ConfigurationManager.AppSettings["EmailHost"],
    //            int.Parse(ConfigurationManager.AppSettings["EmailPort"] ?? "587"),
    //            SecureSocketOptions.StartTls);

    //        smtp.Authenticate(
    //            ConfigurationManager.AppSettings["EmailUsername"],
    //            ConfigurationManager.AppSettings["EmailPassword"]);

    //        smtp.Send(email);
    //        smtp.Disconnect(true);
    //    }


    public class MailService : IMailSendService
    {
        public void SendEmail(EmailDto request)
        {
            var email = new MailMessage();
            email.From = new MailAddress(ConfigurationManager.AppSettings["EmailUsername"]);
            email.To.Add(request.To);
            email.Subject = request.Subject;
            email.Body = request.Body;
            email.IsBodyHtml = true;

            using (var smtp = new System.Net.Mail.SmtpClient(
                ConfigurationManager.AppSettings["EmailHost"],
                int.Parse(ConfigurationManager.AppSettings["EmailPort"] ?? "587")))
            {
                smtp.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["EmailUsername"],
                    ConfigurationManager.AppSettings["EmailPassword"]);

                smtp.EnableSsl = true;

                smtp.Send(email);
            }
        }
        }
    }



