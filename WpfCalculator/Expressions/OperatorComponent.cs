using System;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public class OperatorComponent : IMathComponent
    {
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
