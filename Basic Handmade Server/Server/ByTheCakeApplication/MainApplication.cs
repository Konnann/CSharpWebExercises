namespace MyServer.ByTheCakeApplication
{
    using Server.Handlers;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using ByTheCakeApplication.Controllers;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute(
                "/", 
                new GetHandler(
                    req => new HomeController()
                    .Index()));

            appRouteConfig.AddRoute(
                "/about",
                new GetHandler(
                    req => new HomeController()
                    .About()));

            appRouteConfig.AddRoute(
                "/add",
                new GetHandler(
                    req => new CakesController()
                    .Add()));

            appRouteConfig.AddRoute(
                "/add",
                new PostHandler(
                    req => new CakesController()
                    .Add(req.FormData["name"], req.FormData["price"])));

            appRouteConfig.AddRoute(
                "/search",
                new GetHandler(
                    req => new CakesController()
                    .Search(req.UrlParameters)));
        }
    }
}
