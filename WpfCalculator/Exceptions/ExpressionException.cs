using System;

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
        public InvalidExpressionException(Expressions.Expression expression) : base(string.Format($"{expression} is not valid"))
        {

        }
    }

    public class InvalidExpressionSyntaxException : ExpressionException
    {
        public InvalidExpressionSyntaxException(string message) : base(message)
        {

        }
    }

    public class UnknownOperatorException : ExpressionException
    {
        public UnknownOperatorException(char operatorCharacter) : base($"Unknown operator {operatorCharacter}")
        {

        }
    }

    public class UnknownFunctionException : ExpressionException
    {
        public UnknownFunctionException(string function) : base($"Unknown function {function}")
        {

        }
    }
}
