namespace MyServer.CalculatorApplication.Controllers
{
    using Enums;
    using Model;
    using MyServer.CalculatorApplication.Handlers;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        public IHttpResponse Calculator()
        {
            return this.FileViewResponse(@"calculator");
        }

        public IHttpResponse Calculator(IDictionary<string, string> formData)
        {
            var a = decimal.Parse(formData["a"]);
            var symbol = (MathSymbol)int.Parse(formData["symbol"]);
            var b = decimal.Parse(formData["b"]);

            var equation = new EquationModel(a, symbol, b);
            var result = new CalculationsHandler().Handle(equation).ToString();

            return FileViewResponse(@"calculator", @"calculator--result", new Dictionary<string, string>
            {
                ["resultNum"] = result 
            });
        }
    }
}
