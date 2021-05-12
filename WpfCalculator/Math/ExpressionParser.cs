using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class ExpressionParser
    {
        public static readonly Regex NUMBER_COMPONENT_REGEX = new Regex("[0-9.]");
        private bool expectsNumber;
        private ExpressionBuilder expressionBuilder;

        public ExpressionParser()
        {
            expectsNumber = true;
            expressionBuilder = new ExpressionBuilder();
        }

        public Expression Parse(string expression)
        {
            int length = expression.Length;
            int readerIndex;
            StartReading(expression, out readerIndex);
            while (readerIndex < length)
            {
                if (expectsNumber)
                {
                    // number or function
                    ParseValueType(expression, ref readerIndex);
                    expectsNumber = false;
                }
                else
                {
                    // operator
                }
            }
            return null;
        }

        public void ParseValueType(string expression, ref int readerIndex)
        {
            // number / function / expression
        }

        public void ParseOperator(string expression, ref int readerIndex)
        {

        }

        public double ReadNumber(string expression, ref int readerIndex)
        {
            StringBuilder builder = new StringBuilder();
            bool valid = true;
            while (valid && readerIndex < expression.Length)
            {
                char character = expression[readerIndex];
                if (NUMBER_COMPONENT_REGEX.IsMatch(character.ToString()))
                {
                    builder.Append(character);
                }
                else
                {
                    valid = false;
                }
                ++readerIndex;
            }
            double result;
            if (!double.TryParse(builder.ToString(), out result))
            {
                throw new InvalidExpressionSyntaxException("Number expected");
            }
            return result;
        }

        public double StartReading(string expression, out int readerIndex)
        {
            readerIndex = 0;
            bool negative = false;
            if (expression[readerIndex] == '-')
            {
                negative = true;
                ++readerIndex;
            }
            double number = ReadNumber(expression, ref readerIndex);
            if (negative)
                number = -number;
            return number;
        }
    }
}
