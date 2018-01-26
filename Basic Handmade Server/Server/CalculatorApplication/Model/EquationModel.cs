namespace MyServer.CalculatorApplication.Model
{
    using MyServer.CalculatorApplication.Enums;

    public class EquationModel
    {
        public EquationModel(decimal a, MathSymbol symbol, decimal b)
        {
            this.A = a;
            this.B = b;
            this.Symbol = symbol;
        }

        public decimal A { get; private set; }

        public decimal B  { get; private set; }

        public MathSymbol Symbol { get; private set; }
    }
}
