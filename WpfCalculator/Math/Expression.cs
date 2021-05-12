using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class Expression : IMathComponent
    {
        private List<IMathComponent> expressionComponents;

        public Expression()
        {
            expressionComponents = new List<IMathComponent>();
        }

        public double GetValue()
        {
            double result = 0;
            foreach (IMathComponent component in expressionComponents)
            {
                result += component.GetValue();
            }
            return result;
        }

        public bool IsFunction()
        {
            return false;
        }
    }
}
