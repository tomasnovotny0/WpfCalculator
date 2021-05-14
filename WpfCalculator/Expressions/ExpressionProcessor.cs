using System.Text.RegularExpressions;

namespace WpfCalculator.Expressions
{
    public delegate IExpressionParser ConstructNewParser();

    public class ExpressionProcessor
    {
        public ConstructNewParser ParserFactory { get; }

        public ExpressionProcessor(ConstructNewParser parserFactory)
        {
            ParserFactory = parserFactory;
        }

        public Expression ProcessExpression(string expression)
        {
            IExpressionParser parser = ParserFactory.Invoke();
            parser.ParserFactory = ParserFactory;
            return parser.Parse(Regex.Replace(expression.ToLower(), @"\s+", ""));
        }
    }
}
