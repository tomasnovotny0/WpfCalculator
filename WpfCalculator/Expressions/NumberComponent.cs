using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Expressions
{
    public class NumberComponent : IMathComponent
    {
        public double Value { get; }
        public bool Negative { get; set; }

        public NumberComponent(double value)
        {
            Value = value;
        }
        
        public double GetValue()
        {
            return Negative ? -Value : Value;
        }
    }
}
