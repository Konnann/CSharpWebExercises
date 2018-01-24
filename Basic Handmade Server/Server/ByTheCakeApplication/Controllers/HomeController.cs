namespace MyServer.ByTheCakeApplication.Controllers
{
    using Helpers;
    using Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"Home\index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse(@"Home\about");
        }
    }
}
