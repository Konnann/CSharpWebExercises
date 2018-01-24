namespace MyServer.Server.HTTP
{
    using Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(IHttpRequest requestString)
        {
            this.request = requestString;
        }
 
        public IHttpRequest Request => this.request;
    }
}
