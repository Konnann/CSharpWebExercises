namespace MyServer
{
    using Server;
    using Server.Routing;
    using Server.Contracts;
    using CalculatorApplication;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            //launch SendEmailApplicationApp

            //var app = new SendEmailApplication.MainApplication();

            //launch calculator App
            //var app = new CalculatorApplication.MainApplication();

            //Launch Cakes App
            var app = new ByTheCakeApplication.MainApplication();

            var routeConfig = new AppRouteConfig();
            app.Configure(routeConfig);

            this.webServer = new WebServer(1337, routeConfig);
            this.webServer.Run();
        }
    }
}
