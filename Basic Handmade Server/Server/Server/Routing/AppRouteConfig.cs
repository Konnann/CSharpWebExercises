namespace MyServer.Server.Routing
{
    using Enums;
    using System;
    using Handlers;
    using Contracts;
    using System.Linq;
    using System.Collections.Generic;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }

            this.Routes = routes;
        }

        public void AddRoute(string route, RequestHandler handler)
        {
            var handlerName = handler.GetType().Name.ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[HttpRequestMethod.GET].Add(route, handler);
            }
            else if (handlerName.Contains("post"))
            {
                this.routes[HttpRequestMethod.POST].Add(route, handler);
            }
            else
            {
                throw new InvalidOperationException("Invalid handler.");
            }
        }
    }
}
