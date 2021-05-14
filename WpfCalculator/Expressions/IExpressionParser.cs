namespace WpfCalculator.Expressions
{
    public interface IExpressionParser
    {
        Expression Parse(string input);

        void ParseValue(string expression, ref int readerIndex, bool isNegativeValue);

        void ParseOperator(string expression, ref int readerIndex);
    }
}
