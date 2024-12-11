
using System.Net;
using System.Net.Mail;
using AuthenticationMs.Common.Exceptions;

namespace AuthenticationMs.Infrastructure
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("tusgruasucab@gmail.com", "nrzq hpmb ghbj kozm"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("tusgruasucab@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                //TODO: Tendra algo que ver con colas?
                throw new EmailSenderException("Error sending email", ex);
            }
        }
    }
}