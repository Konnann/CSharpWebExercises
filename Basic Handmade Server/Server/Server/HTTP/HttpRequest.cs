namespace MyServer.Server.HTTP
{
    using Enums;
    using Common;
    using System;
    using Contracts;
    using Exceptions;
    using System.Net;
    using System.Linq;
    using System.Collections.Generic;

    public class HttpRequest : IHttpRequest
    {
        private string requestString;

        public HttpRequest(string requestString)
        {
            this.Headers = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public IHttpCookieCollection Cookies { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            this.requestString = requestString;
            string[] requestLines = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = requestLines[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if(requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if(this.RequestMethod == HttpRequestMethod.GET)
            {
                this.UrlParameters = QueryParameters;
            }
            else if(this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }
        }
        
        private void ParseCookies()
        {
            if (this.Headers.ContainsKey(HttpHeader.Cookie))
            {
                var allCookies = this.Headers.GetHeader(HttpHeader.Cookie);

                foreach (var cookie in allCookies)
                {
                    var cookieParts = cookie
                        .Value
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault();

                    if(cookie == null || !cookieParts.Contains('='))
                    {
                        continue;
                    }

                    var cookieKeyValuePair = cookieParts.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if(cookieKeyValuePair.Length == 2)
                    {
                        var key = cookieKeyValuePair[0];
                        var value = cookieKeyValuePair[1];

                        this.Cookies.Add(new HttpCookie(key, value, false));
                    }

                }
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("/"))
            {
                return;
            }

            string query = this.Url.Split('/')[1];
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split(new char[] { '&', '?'});
            foreach (var queryPair in queryPairs)
            {
                string[] queryArgs = queryPair.Split('=');
                if (queryArgs.Length != 2)
                {
                    continue;
                }

                dict.Add(
                    WebUtility.UrlDecode(queryArgs[0]),
                    WebUtility.UrlDecode(queryArgs[1])
                    );
            }
           
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 0; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new string[] { ": " }, StringSplitOptions.None);

                if (headerArgs.Length == 2)
                {
                    var header = new HttpHeader(headerArgs[0], headerArgs[1]);
                    this.Headers.Add(header);
                }
            }

            if(!Headers.ContainsKey("Host"))
            {
                throw new Exception();
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            object result;
            bool isValid = Enum.TryParse(typeof(HttpRequestMethod), method, out result);

            if (!isValid)
            {
                throw new BadRequestException("Invalid request method.");
            }

            return (HttpRequestMethod)result;
        }

        public override string ToString()
        {
            return this.requestString;
        }
    }
}
