namespace MyServer.Server.HTTP.Response
{
    using Enums;
    using Common;
    using Contracts;
    using System.Text;
    using Server.Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        protected readonly IView view;

        protected HttpStatusCode StatusCode { get; set; }

        protected HttpResponse() { }

        protected HttpResponse(string redirectUrl)
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

        protected HttpResponse(HttpStatusCode responseCode, IView view)
        {
            this.view = view;
            this.StatusCode = responseCode;
        }

        public IHttpHeaderCollection Headers { get; set; } = new HttpHeaderCollection();
        
        public IHttpCookieCollection Cookies { get; set; } = new HttpCookieCollection();

        public void AddHeader(string key, string value)
        {
            var header = new HttpHeader(key, value);
            this.Headers.Add(header);
        }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();
            response.AppendLine($"HTTP/1.1 { (int)this.StatusCode} {this.StatusCode}");
            response.AppendLine(this.Headers.ToString());
            

            //if ((int)this.StatusCode < 300 || (int)this.StatusCode > 500)
            //{
            //    TODO: change status code to 400 as top border when
            //    response.AppendLine(this.view.View());
            //}

            return response.ToString();
        }
    }
}
