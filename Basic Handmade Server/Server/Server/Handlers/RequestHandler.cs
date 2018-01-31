namespace MyServer.Server.Handlers
{
    using System;
    using Common;
    using Contracts;
    using HTTP.Contracts;
    using MyServer.Server.HTTP;

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

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.AddHeader(HttpHeader.ContentType, "text/html");
            }

            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(new HttpHeader(HttpHeader.SetCookie, cookie.ToString()));
            }

            return response;
        }
    }
}
