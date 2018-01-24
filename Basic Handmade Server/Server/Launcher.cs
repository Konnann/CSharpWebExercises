namespace MyServer
{
    using Server;
    using Server.Routing;
    using Server.Contracts;
    using ByTheCakeApplication;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var app = new MainApplication();
            var routeConfig = new AppRouteConfig();
            app.Configure(routeConfig);

            this.webServer = new WebServer(1337, routeConfig);
            this.webServer.Run();
        }
    }
}
