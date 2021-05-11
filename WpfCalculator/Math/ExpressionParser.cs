using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Math
{
    public class ExpressionParser
    {
        private readonly string expression;
        private bool expectsNumber;

        public ExpressionParser(string expressionString)
        {
            expression = expressionString;
            expectsNumber = true;
        }

        public Expression Parse()
        {
            return null;
        }
    }
}
