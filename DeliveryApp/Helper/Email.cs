using System.Net;
using System.Net.Mail;

namespace DeliveryApp.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string displayName = _configuration.GetValue<string>("SMTP:DisplayName");
                string username = _configuration.GetValue<string>("SMTP:Username");
                string password = _configuration.GetValue<string>("SMTP:Password");
                int port = _configuration.GetValue<int>("SMTP:Port");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, displayName)
                };

                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;

                }

            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
