namespace MyServer.SendEmailApplication.Controllers
{
    using System.IO;
    using Views;
    using Server.Enums;
    using Server.HTTP.Response;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public abstract class Controller
    {
        private static string path = @"SendEmailApplication\Resources\{0}.html";

        public IHttpResponse FileViewResponse(string fileName)
        {
            var fileHtml = this.ReadFile(fileName);

            if (fileHtml.Contains(@"{{{"))
            {
                fileHtml = Regex.Replace(fileHtml, @"{{{[a-zA-Z]+}}}", "");
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(fileHtml));
        }

        public IHttpResponse FileViewResponse(string fileName, Dictionary<string, string> values)
        {
            var file = this.ReadFile(fileName);
            var result = this.ReplacePlaceholders(file, values);

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        public IHttpResponse FileViewResponse(string firstFileName, string secondFileName, Dictionary<string, string> values)
        {
            var firstFileHtml = this.ReadFile(firstFileName);
            var secondFileHtml = this.ReadFile(secondFileName);

            var result = this.ReplacePlaceholders(firstFileHtml, new Dictionary<string, string>
            {
                ["content"] = secondFileHtml
            });

            result = this.ReplacePlaceholders(result, values);

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        private string ReadFile(string fileName)
        {
            return File.ReadAllText(string.Format(path, fileName));
        }

        private string ReplacePlaceholders(string fileHtml, Dictionary<string, string> values)
        {
            var replaced = fileHtml;

            if (values != null && values.Any())
            {
                foreach (var value in values)
                {
                    replaced = replaced.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }
            return replaced;
        }
    }
}
