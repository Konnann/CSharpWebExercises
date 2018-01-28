namespace MyServer.SendEmailApplication.Handlers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;

    public class EmailHandler
    {
        private static string email = "testemail00987@gmail.com";
        private static string password = "00112233";

        public void SendEmail(IDictionary<string, string> emailData)
        {
            var fromAddress = new MailAddress(email, "From Name");
            var toAddress = new MailAddress(emailData["recipient"], emailData["recipient"]);
            var fromPassword = password;
            var subject = emailData["subject"];
            var body = emailData["message"];

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
