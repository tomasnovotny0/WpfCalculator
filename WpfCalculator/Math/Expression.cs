using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class Expression : IMathComponent
    {
        private LinkedList<IMathComponent> expressionComponents;

        public Expression() : this(new LinkedList<IMathComponent>())
        {
            
        }

        public Expression(LinkedList<IMathComponent> components)
        {
            expressionComponents = components;
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

        public bool IsValueType()
        {
            return true;
        }
    }
}
