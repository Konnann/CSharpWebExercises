namespace MyServer.Server.HTTP
{
    using System;
    using Common;
    using Contracts;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            if (!this.headers.ContainsKey(header.Key))
            {
                this.headers[header.Key] = new List<HttpHeader>();
            }

            this.headers[header.Key].Add(header);
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            //if (!this.headers.ContainsKey(key))
            //{
            //    throw new InvalidOperationException($"The given key {key} is not present in the given headers collection.");
            //}

            return this.headers.ContainsKey(key);
        }

        public ICollection<HttpHeader> GetHeader(string key)
        {
            return this.headers[key];
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var h in this.headers)
            {
                var headerKey = h.Key;

                foreach (var headerValue in h.Value)
                {
                    result.AppendLine($"{headerKey}: {headerValue.Value}");
                }
            }

            return result.ToString();
        }

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.headers.Values.GetEnumerator();
    }
}
