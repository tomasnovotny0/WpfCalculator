namespace WpfCalculator.Expressions
{
    public sealed class OperatorInstance : IOperation
    {
        public Operator Operator { get; }
        private readonly IMathComponent number1, number2;

        public OperatorInstance(IMathComponent number1, IMathComponent number2, Operator @operator)
        {
            Operator = @operator;
            this.number1 = number1;
            this.number2 = number2;
        }

        public double Calculate()
        {
            return Operator.Combine(number1, number2);
        }
    }
}
