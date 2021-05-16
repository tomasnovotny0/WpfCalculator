using System.Text;
using System.Text.RegularExpressions;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Parser implementation which can parse
    /// <list type="bullet">
    /// <item>
    ///     <description>Raw numbers</description>
    /// </item>
    /// <item>
    ///     <description>Functions</description>
    /// </item>
    /// <item>
    ///     <description>Expressions</description>
    /// </item>
    /// <item>
    ///     <description>Operators</description>
    /// </item>
    /// </list>
    /// </summary>
    public class SimpleExpressionParser : IExpressionParser
    {
        public static readonly Regex NUMBER_COMPONENT_REGEX = new Regex("[0-9.]");
        public static readonly Regex VALID_FUNCTION_CHARACTERS = new Regex("[a-zA-Z]");
        protected ExpressionBuilder expressionBuilder;
        private bool ReadingValue;

        public Expression Parse(string expression, ConstructNewParser instanceCreator)
        {
            expressionBuilder = new ExpressionBuilder();
            int len = expression.Length;
            StartReading(expression, out int readerIndex, instanceCreator);
            while (readerIndex < len)
            {
                if (ReadingValue)
                {
                    ParseValue(expression, ref readerIndex, false, instanceCreator);
                    ReadingValue = false;
                }
                else
                {
                    ParseOperator(expression, ref readerIndex);
                    ReadingValue = true;
                }
            }
            return expressionBuilder.Build();
        }

        /// <summary>
        /// Reads first component of <paramref name="expression"/>. Handles negative expressions
        /// </summary>
        /// <param name="expression">Expression which is being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <param name="instanceCreator">Parser instance creator for parsing sub-expressions</param>
        protected virtual void StartReading(string expression, out int readerIndex, ConstructNewParser instanceCreator)
        {
            readerIndex = 0;
            if (expression.Length == 0)
                return;
            if (expression[readerIndex] == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex, false, instanceCreator);
                return;
            }
            bool negative = false;
            if (expression[readerIndex] == '-')
            {
                negative = true;
                ++readerIndex;
            }
            ParseValue(expression, ref readerIndex, negative, instanceCreator);
        }

        /// <summary>
        /// Reads value from expression.
        /// </summary>
        /// <param name="expression">Expression which is being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <param name="negative">Whether parsed value should be negative</param>
        /// <param name="instanceCreator">Parser instance creator for parsing sub-expressions</param>
        protected virtual void ParseValue(string expression, ref int readerIndex, bool negative, ConstructNewParser instanceCreator)
        {
            // number / function / expression
            if (readerIndex >= expression.Length)
                return;
            char firstCharacter = expression[readerIndex];
            if (firstCharacter == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex, negative, instanceCreator);
            }
            else if (char.IsDigit(firstCharacter))
            {
                ReadNumber(expression, ref readerIndex, negative);
            }
            else if (VALID_FUNCTION_CHARACTERS.IsMatch(firstCharacter.ToString()))
            {
                ParseFunction(expression, ref readerIndex, negative, instanceCreator);
            }
        }

        /// <summary>
        /// Reads operator from expression
        /// </summary>
        /// <param name="expression">Expression which is being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        protected virtual void ParseOperator(string expression, ref int readerIndex)
        {
            char character = expression[readerIndex];
            Operator @operator = Operators.FindOperator(character);
            expressionBuilder.Operator(@operator);
            ++readerIndex;
        }

        /// <summary>
        /// Returns expression which is inside brackets.
        /// Expression (x+y) will return x+y string
        /// </summary>
        /// <param name="expression">Expression which is being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reads function from expression string
        /// </summary>
        /// <param name="expression">Expression which is being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <param name="negative">Whether function value should be negative</param>
        /// <param name="instanceCreator">Parser instance creator for parsing sub-expressions</param>
        protected void ParseFunction(string expression, ref int readerIndex, bool negative, ConstructNewParser instanceCreator)
        {
            StringBuilder functionString = new StringBuilder();
            functionString.Append(expression[readerIndex++]);
            if (readerIndex >= expression.Length)
            {
                ParseNoParameterFunction(functionString.ToString(), negative, instanceCreator);
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
                        ParseNoParameterFunction(functionString.ToString(), negative, instanceCreator);
                        break;
                    }
                }
                else if (character == '(')
                {
                    Function function = Functions.FindFunction(functionString.ToString());
                    readerIndex++;
                    string parameters = GetExpression(expression, ref readerIndex);
                    expressionBuilder.Function(function, parameters, negative, instanceCreator);
                    break;
                }
                else
                {
                    ParseNoParameterFunction(functionString.ToString(), negative, instanceCreator);
                    break;
                }
            }
            
        }

        /// <summary>
        /// Reads function which has no parameters
        /// </summary>
        /// <param name="funcName">Function key - such as <code>sin</code></param>
        /// <param name="negative">Whether function value should be negative</param>
        /// <param name="instanceCreator">Parser instance creator for parsing sub-expressions</param>
        protected void ParseNoParameterFunction(string funcName, bool negative, ConstructNewParser instanceCreator)
        {
            Function function = Functions.FindFunction(funcName);
            if (function.InputCount != 0) throw new InvalidExpressionSyntaxException("Invalid function syntax");
            expressionBuilder.Function(function, "", negative, instanceCreator);
        }

        /// <summary>
        /// Reads number from expression
        /// </summary>
        /// <param name="expression">Expression being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <param name="negative">Whether number should be negative</param>
        protected void ReadNumber(string expression, ref int readerIndex, bool negative)
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
            if (!double.TryParse(builder.ToString(), out double result))
            {
                throw new InvalidExpressionSyntaxException("Number expected");
            }
            expressionBuilder.Number(result, negative);
        }

        /// <summary>
        /// Reads sub-expression from <paramref name="expression"/>
        /// </summary>
        /// <param name="expression">Expression being parsed</param>
        /// <param name="readerIndex">Position in expression</param>
        /// <param name="negative">Whether expression value should be negative</param>
        /// <param name="instanceCreator">Parser instance creator for parsing sub-expressions</param>
        protected void ParseExpression(string expression, ref int readerIndex, bool negative, ConstructNewParser instanceCreator)
        {
            expressionBuilder.Expression(GetExpression(expression, ref readerIndex), negative, instanceCreator);
        }
    }
}
