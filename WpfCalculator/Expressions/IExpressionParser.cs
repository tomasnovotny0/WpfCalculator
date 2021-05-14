namespace WpfCalculator.Expressions
{
    public interface IExpressionParser
    {
        ConstructNewParser ParserFactory { get; set; }

        Expression Parse(string expression);

        void ParseValue(string expression, ref int readerIndex, bool isNegativeValue);

        void ParseOperator(string expression, ref int readerIndex);
    }
}
