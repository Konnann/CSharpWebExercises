namespace MyServer.Server.HTTP.Response
{
    using System.Collections.Generic;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl) : base (redirectUrl)
        { }
    }
}
