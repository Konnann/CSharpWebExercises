namespace MyServer.ByTheCakeApplication.Controllers
{
    using Helpers;
    using MyServer.Server.HTTP;
    using Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            var response = this.FileViewResponse(@"Home\index");

            response.Cookies.Add(new HttpCookie("lang", "en"));

            return response;
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse(@"Home\about");
        }
    }
}
