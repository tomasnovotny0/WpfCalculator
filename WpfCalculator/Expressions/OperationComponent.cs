using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Expressions
{
    public class OperationComponent : IMathComponent
    {
        public bool Negative { get; set; }
        public IOperation Operation { get; }

        public OperationComponent(IOperation operation)
        {
            Operation = operation;
        }

        public double GetValue()
        {
            return Negative ? -Operation.Calculate() : Operation.Calculate();
        }
    }
}
