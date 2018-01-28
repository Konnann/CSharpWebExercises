namespace MyServer.SendEmailApplication
{
    using MyServer.SendEmailApplication.Controllers;
    using MyServer.Server.Handlers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute(
                "/login",
                new GetHandler(
                    req => new HomeController()
                    .Login(req.UrlParameters)));

            appRouteConfig.AddRoute(
                "/sendemail",
                new GetHandler(
                    req => new HomeController()
                    .SendEmail()));

            appRouteConfig.AddRoute(
                "/sendemail",
                new PostHandler(
                    req => new HomeController()
                    .SendingEmail(req.FormData)));

            appRouteConfig.AddRoute(
                "/sent",
                new GetHandler(
                    req => new HomeController()
                    .Sent()));
        }
    }
}
