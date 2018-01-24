namespace MyServer.ByTheCakeApplication.Controllers
{
    using Helpers;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using ByTheCakeApplication.Models;
    using System.IO;
    using System;
    using System.Linq;

    public class CakesController : Controller
    {
        private static List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            return this.FileViewResponse(@"Cakes\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);
            var data = new Dictionary<string, string>()
            {
                ["name"] = name,
                ["price"] = price
            };

            using (var streamWriter = new StreamWriter(@"ByTheCakeApplication\Data\database.csv", true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse(@"Cakes\add", @"Cakes\add--new-cake", data);
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            const string searchTermKey = "searchTerm";

            var results = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                var savedCakesAsString = File
                    .ReadAllLines(@"ByTheCakeApplication\Data\database.csv")
                    .Where(l => l.Contains(','))
                    .Select(l => l.Split(','))
                    .Select(l => new Cake
                    {
                        Name = l[0],
                        Price = decimal.Parse(l[1])
                    })
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price}</div>");

                if(savedCakesAsString.Count() == 0)
                {
                    results = "No results found.";
                }
                else
                {
                    results = String.Join(Environment.NewLine, savedCakesAsString);
                }
                
                return this.FileViewResponse(@"Cakes\search", new Dictionary<string, string>
                {
                    ["results"] = results
                });
            }

            return this.FileViewResponse(@"Cakes\search");
        }

    }
}
