using System;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Math component containing <see cref="Expressions.Operator"/> object
    /// </summary>
    public class OperatorComponent : IMathComponent
    {
        /// <summary>
        /// Operator of this component
        /// </summary>
        public Operator Operator { get; }
        public bool Negative { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OperatorComponent(Operator @operator)
        {
            Operator = @operator;
        }

        public double GetValue()
        {
            throw new InvalidExpressionSyntaxException("Invalid expression start");
        }
    }
}
