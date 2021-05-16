namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Defines specific operation between two numbers using <see cref="Expressions.Operator"/> object
    /// </summary>
    public sealed class OperatorInstance : IOperation
    {
        /// <summary>
        /// Operator which is used by this operation
        /// </summary>
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
