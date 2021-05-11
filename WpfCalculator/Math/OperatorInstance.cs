using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Math
{
    public sealed class OperatorInstance : IOperation
    {
        public Operator Operator { get; }
        private IMathComponent number1, number2;

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
