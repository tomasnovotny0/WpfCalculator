using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Defines specific operation by using <see cref="Expressions.Function"/> object.
    /// </summary>
    public sealed class FunctionInstance : IOperation
    {
        /// <summary>
        /// Function type of this component
        /// </summary>
        public Function Function { get; }
        private readonly IMathComponent[] parameters;

        public FunctionInstance(Function function, IMathComponent[] parameters)
        {
            Function = function;
            this.parameters = parameters;
            if (parameters.Length != Function.InputCount)
                throw new InvalidExpressionSyntaxException("Invalid parameter count");
        }

        public double Calculate()
        {
            return Function.Calculate(parameters);
        }
    }
}
