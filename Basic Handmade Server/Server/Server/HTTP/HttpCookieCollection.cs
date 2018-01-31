namespace MyServer.Server.HTTP
{
    using Contracts;
    using MyServer.Server.Common;
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class HttpCookieCollection : IHttpCookieCollection 
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            this.cookies[cookie.Key] = cookie;
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.cookies.ContainsKey(key);

        }

        public HttpCookie Get(string key)
        {
            if (!this.cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the cookies collection.");
            }

            return this.cookies[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.cookies.Values.GetEnumerator();
    }
}
