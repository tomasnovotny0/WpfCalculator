using System.Text;
using System.Text.RegularExpressions;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public class ExpressionParser : IExpressionParser
    {
        public static readonly Regex NUMBER_COMPONENT_REGEX = new Regex("[0-9.]");
        public static readonly Regex VALID_FUNCTION_CHARACTERS = new Regex("[a-zA-Z]");
        public ConstructNewParser ParserFactory { get; set; }
        private ExpressionBuilder expressionBuilder;
        private bool ReadingValue;

        public Expression Parse(string expression)
        {
            expressionBuilder = new ExpressionBuilder(ParserFactory);
            int len = expression.Length;
            StartReading(expression, out int readerIndex);
            while (readerIndex < len)
            {
                if (ReadingValue)
                {
                    ParseValue(expression, ref readerIndex, false);
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

        public virtual void StartReading(string expression, out int readerIndex)
        {
            readerIndex = 0;
            if (expression.Length == 0)
                return;
            if (expression[readerIndex] == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex, false);
                return;
            }
            bool negative = false;
            if (expression[readerIndex] == '-')
            {
                negative = true;
                ++readerIndex;
            }
            ParseValue(expression, ref readerIndex, negative);
        }

        public virtual void ParseValue(string expression, ref int readerIndex, bool negative)
        {
            // number / function / expression
            if (readerIndex >= expression.Length)
                return;
            char firstCharacter = expression[readerIndex];
            if (firstCharacter == '(')
            {
                ++readerIndex;
                ParseExpression(expression, ref readerIndex, negative);
            }
            else if (char.IsDigit(firstCharacter))
            {
                ReadNumber(expression, ref readerIndex, negative);
            }
            else if (VALID_FUNCTION_CHARACTERS.IsMatch(firstCharacter.ToString()))
            {
                ParseFunction(expression, ref readerIndex, negative);
            }
        }

        public virtual void ParseOperator(string expression, ref int readerIndex)
        {
            char character = expression[readerIndex];
            Operator @operator = Operators.FindOperator(character);
            expressionBuilder.Operator(@operator);
            ++readerIndex;
        }

        public Expression GetResult()
        {
            return expressionBuilder.Build();
        }

        public virtual void ClearState()
        {

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

        private void ParseFunction(string expression, ref int readerIndex, bool negative)
        {
            StringBuilder functionString = new StringBuilder();
            functionString.Append(expression[readerIndex++]);
            if (readerIndex >= expression.Length)
            {
                ParseNoParameterFunction(functionString.ToString(), negative);
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
                        ParseNoParameterFunction(functionString.ToString(), negative);
                        break;
                    }
                }
                else if (character == '(')
                {
                    Function function = Functions.FindFunction(functionString.ToString());
                    readerIndex++;
                    string parameters = GetExpression(expression, ref readerIndex);
                    expressionBuilder.Function(function, parameters, negative);
                    break;
                }
                else
                {
                    ParseNoParameterFunction(functionString.ToString(), negative);
                    break;
                }
            }
            
        }

        private void ParseNoParameterFunction(string funcName, bool negative)
        {
            Function function = Functions.FindFunction(funcName);
            if (function.InputCount != 0) throw new InvalidExpressionSyntaxException("Invalid function syntax");
            expressionBuilder.Function(function, "", negative);
        }

        private void ReadNumber(string expression, ref int readerIndex, bool negative)
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

        private void ParseExpression(string expression, ref int readerIndex, bool negative)
        {
            expressionBuilder.Expression(GetExpression(expression, ref readerIndex), negative);
        }
    }
}
