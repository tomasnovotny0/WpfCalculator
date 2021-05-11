using System;
using WpfCalculator.Math;

namespace WpfCalculator.Exceptions
{
    public abstract class ExpressionException : Exception
    {
        public ExpressionException(string message) : base(message)
        {
            
        }
    }

    public class InvalidExpressionException : ExpressionException
    {
        public InvalidExpressionException(Expression expression) : base(string.Format($"{expression} is not valid"))
        {

        }
    }
}
