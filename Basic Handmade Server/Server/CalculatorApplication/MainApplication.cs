namespace MyServer.CalculatorApplication
{
    using MyServer.Server.Handlers;
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute(
                "/calculate",
                new GetHandler(
                    req => new HomeController()
                    .Calculator()));

            appRouteConfig.AddRoute(
                "/calculate",
                new PostHandler(
                    req => new HomeController()
                    .Calculator(req.FormData)));
        }
    }
}
