namespace MyServer.Server.Handlers
{
    using System;
    using Common;
    using Contracts;
    using HTTP.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));

            this.handlingFunc = handlingFunc;
        }
            
        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var response = this.handlingFunc(httpContext.Request);

            response.AddHeader("Content-Type", "text/html");

            return response;
        }
    }
}
