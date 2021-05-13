using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class Expression : IMathComponent
    {
        private IMathComponent componentTree;

        public Expression(IMathComponent componentTree)
        {
            this.componentTree = componentTree;
        }

        public double GetValue()
        {
            return componentTree == null ? 0 : componentTree.GetValue();
        }

        public bool IsValueType()
        {
            return true;
        }
    }
}
