using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Math
{
    public class NumberComponent : IMathComponent
    {
        public double Value { get; }

        public NumberComponent(double value)
        {
            Value = value;
        }
        
        public double GetValue()
        {
            return Value;
        }

        public bool IsFunction()
        {
            return false;
        }
    }
}
