namespace MyServer.Server.Handlers
{
    using Common;
    using Contracts;
    using HTTP.Response;
    using HTTP.Contracts;
    using Routing.Contracts;
    using System.Text.RegularExpressions;

    public class HttpHandler : IRequestHandler
    {
        private IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig routeConfig)
       {
            CoreValidator.ThrowIfNull(routeConfig, nameof(routeConfig));

            this.serverRouteConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var requestMethod = context.Request.RequestMethod;
            var requestPath = context.Request.Path;

            foreach (var registeredRoute in this.serverRouteConfig.Routes[requestMethod])
            {
                var routePattern = registeredRoute.Key;
                var routingContext = registeredRoute.Value;

                var routeRegex = new Regex(routePattern);
                var match = routeRegex.Match(requestPath);

                if (!match.Success)
                {
                    continue;
                }

                var parameters = routingContext.Parameters;

                foreach (var parameter in parameters)
                {
                    var paramValue = match.Groups[parameter].Value;
                    context.Request.AddUrlParameter(parameter, paramValue);
                }

                return routingContext.Handler.Handle(context);
            }

            return new NotFoundResponse();
        }
    }
}
