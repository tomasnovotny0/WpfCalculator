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
        public static readonly char FUNCTION_PARAMETER_SEPARATOR = ',';
        public static readonly Regex NUMBER_COMPONENT_REGEX = new Regex("[0-9.]");
        public static readonly Regex VALID_FUNCTION_CHARACTERS = new Regex("[a-zA-Z]");
        private bool expectsNumber;
        private ExpressionBuilder expressionBuilder;

        public ExpressionParser()
        {
            expectsNumber = true;
            expressionBuilder = new ExpressionBuilder();
        }

        /*
         * Known issues:
         * - incorrect value when expression starts with '-' character
         */
        public Expression Parse(string expression)
        {
            int length = expression.Length;
            StartReading(expression, out int readerIndex);
            while (readerIndex < length)
            {
                if (expectsNumber)
                {
                    ParseValueType(expression, ref readerIndex);
                    expectsNumber = false;
                }
                else
                {
                    ParseOperator(expression, ref readerIndex);
                    expectsNumber = true;
                }
            }
            return expressionBuilder.Build();
        }

        public void ParseValueType(string expression, ref int readerIndex)
        {
            // number / function / expression
            if (readerIndex >= expression.Length)
                return;
            char firstCharacter = expression[readerIndex];
            if (firstCharacter == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex);
            }
            else if (char.IsDigit(firstCharacter))
            {
                ReadNumber(expression, ref readerIndex, false);
            }
            else if (VALID_FUNCTION_CHARACTERS.IsMatch(firstCharacter.ToString()))
            {
                ParseFunction(expression, ref readerIndex);
            }
        }

        private void ParseFunction(string expression, ref int readerIndex)
        {
            StringBuilder functionString = new StringBuilder();
            functionString.Append(expression[readerIndex++]);
            if (readerIndex >= expression.Length)
            {
                ParseNoParameterFunction(functionString.ToString());
                return;
            }
            while (readerIndex < expression.Length)
            {
                char character = expression[readerIndex];
                if (VALID_FUNCTION_CHARACTERS.IsMatch(character.ToString()))
                {
                    functionString.Append(character);
                    ++readerIndex;

                    if(readerIndex >= expression.Length)
                    {
                        ParseNoParameterFunction(functionString.ToString());
                        break;
                    }
                }
                else if (character == '(')
                {
                    Function function = Functions.FindFunction(functionString.ToString());
                    readerIndex++;
                    string parameters = GetExpression(expression, ref readerIndex);
                    expressionBuilder.Function(function, parameters);
                    break;
                }
                else
                {
                    ParseNoParameterFunction(functionString.ToString());
                    break;
                }
            }
            
        }

        private void ParseNoParameterFunction(string funcName)
        {
            Function function = Functions.FindFunction(funcName);
            if (function.InputCount != 0) throw new InvalidExpressionSyntaxException("Invalid function syntax");
            expressionBuilder.Function(function, "");
        }

        public void ParseOperator(string expression, ref int readerIndex)
        {
            char character = expression[readerIndex];
            Operator @operator = Operators.FindOperator(character);
            expressionBuilder.Operator(@operator);
            ++readerIndex;
        }

        public void ReadNumber(string expression, ref int readerIndex, bool invert)
        {
            StringBuilder builder = new StringBuilder();
            bool valid = true;
            while (valid && readerIndex < expression.Length)
            {
                char character = expression[readerIndex];
                if (NUMBER_COMPONENT_REGEX.IsMatch(character.ToString()))
                {
                    builder.Append(character);
                    ++readerIndex;
                }
                else
                {
                    valid = false;
                }
            }
            double result;
            if (!double.TryParse(builder.ToString(), out result))
            {
                throw new InvalidExpressionSyntaxException("Number expected");
            }
            if (invert)
            {
                result = -result;
            }
            expressionBuilder.Number(result);
        }

        private void StartReading(string expression, out int readerIndex)
        {
            readerIndex = 0;
            if (expression.Length == 0)
                return;
            if (expression[readerIndex] == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex);
                return;
            }
            bool negative = false;
            if (expression[readerIndex] == '-')
            {
                negative = true;
                ++readerIndex;
            }
            ParseValueType(expression, ref readerIndex);
            expectsNumber = false;
        }

        private void ParseExpression(string expression, ref int readerIndex)
        {
            expressionBuilder.Expression(GetExpression(expression, ref readerIndex));
        }

        public static string GetExpression(string expression, ref int readerIndex)
        {
            int beginIndex = readerIndex;
            int offset = 1;

            while (readerIndex < expression.Length)
            {
                char character = expression[readerIndex];
                if (character == '(')
                    ++offset;
                else if (character == ')')
                {
                    --offset;
                    if (offset == 0)
                    {
                        string sub = expression.Substring(beginIndex, readerIndex - beginIndex);
                        ++readerIndex;
                        return sub;
                    }
                }
                ++readerIndex;
            }
            throw new InvalidExpressionSyntaxException("Invalid expression");
        }
    }
}
