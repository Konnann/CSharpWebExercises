namespace MyServer.Server
{
    using Routing;
    using Contracts;
    using System.Net;
    using Routing.Contracts;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private const string localHostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener listener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            this.port = port;

            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);

            this.listener = new TcpListener(IPAddress.Parse(localHostIpAddress), port);
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            System.Console.WriteLine($"Server running on {localHostIpAddress}:{this.port}");

            Task.Run(this.ListenLoop).Wait();
        }

        private async Task ListenLoop()
        {
            while (isRunning)
            {
                var client = await this.listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                await connectionHandler.ProcessRequestAsync();
            }
        }
    }
}
