 namespace MyServer.Server.Routing
{
    using Enums;
    using System;
    using System.Text;
    using System.Linq;
    using Routing.Contracts;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeRouteConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; private set; }

        private void InitializeRouteConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registeredRoute in appRouteConfig.Routes)
            {
                var requestMethod = registeredRoute.Key;
                var routesWithHandlers = registeredRoute.Value;

                foreach (var routeWithHandler in routesWithHandlers)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;
                
                    var parameters = new List<string>();

                    var parsedRouteRegex = this.ParseRoute(route, parameters);

                    var routingContext = new RoutingContext(handler, parameters);
                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext); 
                }
                this.Routes = routes;
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var result = new StringBuilder();
            result.Append("^/");

            if (route == "/")
            {
                result.Append("$");
                return result.ToString();
            }

            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder parsedRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                string end = i == tokens.Length - 1 ? "$" : "/";

                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                var parameterRegex = new Regex("<\\w+>");
                var parameterMatch = parameterRegex.Match(tokens[i]);

                if (!parameterMatch.Success)
                {
                    throw new InvalidOperationExcepton($"Route parameter in '{tokens[i]}' is not valid");
                 
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);

                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = tokens[i].Substring(1, tokens[i].Length - 2);

                parsedRegex.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }
    }
}
