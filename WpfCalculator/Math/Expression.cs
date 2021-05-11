using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class Expression
    {
        private List<IMathComponent> expressionComponents;

        public Expression()
        {
            expressionComponents = new List<IMathComponent>();
        }

        public double Evaluate()
        {
            int count = expressionComponents.Count;
            if (count == 0)
            {
                throw new InvalidExpressionException(this);
            }
            
            return 0;
        }
    }
}
