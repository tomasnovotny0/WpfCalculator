using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Math
{
    public class OperationComponent : IMathComponent
    {
        public IOperation Operation { get; }

        public OperationComponent(IOperation operation)
        {
            Operation = operation;
        }

        public bool IsValueType()
        {
            return true;
        }

        public double GetValue()
        {
            return Operation.Calculate();
        }
    }
}
