using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public sealed class FunctionInstance : IOperation
    {
        public Function Function { get; }
        private IMathComponent[] parameters;

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
