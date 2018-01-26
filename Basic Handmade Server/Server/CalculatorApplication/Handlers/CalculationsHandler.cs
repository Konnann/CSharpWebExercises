namespace MyServer.CalculatorApplication.Handlers
{
    using Enums;
    using Model;

    public class CalculationsHandler
    {
        public decimal Handle(EquationModel equation)
        {
            var result = 0M;
            var a = equation.A;
            var b = equation.B;

            switch (equation.Symbol)
            {
                case MathSymbol.Add:
                    result = this.Add(a, b);
                    break;
                case MathSymbol.Subtract:
                    result = this.Subtract(a, b);
                    break;
                case MathSymbol.Multiply:
                    result = this.Multiply(a, b);
                    break;
                case MathSymbol.Divide:
                    result = this.Divide(a, b);
                    break;
            }

            return result;
        }

        public decimal Add(decimal a, decimal b) => a + b;

        public decimal Subtract(decimal a, decimal b) => a - b;

        public decimal Multiply(decimal a, decimal b) => a * b;

        public decimal Divide(decimal a, decimal b) => a / b;
    }
}
