namespace MyServer.SendEmailApplication.Controllers
{
    using Handlers;
    using System.Net;
    using System.Net.Mail;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using MyServer.Server.HTTP.Response;

    public class HomeController : Controller
    {
        public IHttpResponse Login(IDictionary<string, string> urlData)
        {
            if (!urlData.ContainsKey("username"))
            {
                return this.FileViewResponse("login");
            }

            var username = urlData["username"];
            var password = urlData["password"];
            
            if (new LoginHandler().ValidateLogin(username, password))
            {
                return new RedirectResponse(@"/sendemail");
            }

            return this.FileViewResponse("login", "login--fail", new Dictionary<string, string>());
        }

        public IHttpResponse SendEmail()
        {
            return this.FileViewResponse("send-email");
        }

        public IHttpResponse SendingEmail(IDictionary<string, string> emailData)
        {
            new EmailHandler().SendEmail(emailData); 
            return new RedirectResponse("/sent");
        }

        public IHttpResponse Sent()
        {
            return this.FileViewResponse("sent-success");
        }
    }
}
