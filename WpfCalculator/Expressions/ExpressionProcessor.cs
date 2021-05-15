using System.Text.RegularExpressions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Delegate acting as instance creator for <see cref="IExpressionParser"/>. Or atleast that's intention
    /// of this delegate.
    /// </summary>
    /// <returns><see cref="IExpressionParser"/> implementation</returns>
    public delegate IExpressionParser ConstructNewParser();

    /// <summary>
    /// Expression processor is processing expressions in string format.
    /// Expressions are parsed by supplied <see cref="IExpressionParser"/> which is
    /// constructed inside <see cref="ConstructNewParser"/> which should act as instance creator.
    /// That means you don't need to create new processor instances for different expressions.
    /// </summary>
    public sealed class ExpressionProcessor
    {
        /// <summary>
        /// Instance creator for <see cref="IExpressionParser"/> implementation
        /// </summary>
        public ConstructNewParser InstanceCreator { get; }

        /// <summary>
        /// Constructs new instance of expression processor
        /// </summary>
        /// <param name="instanceCreator"></param>
        public ExpressionProcessor(ConstructNewParser instanceCreator)
        {
            InstanceCreator = instanceCreator;
        }

        /// <summary>
        /// Processes expression in string format.
        /// </summary>
        /// <param name="expression">Expression which will be parsed. Expression is converted to lower case characters and all whitespaces are removed</param>
        /// <returns>Processed expression or throws exception based on <see cref="IExpressionParser"/> implementation</returns>
        public Expression ProcessExpression(string expression)
        {
            IExpressionParser parser = InstanceCreator.Invoke();
            parser.ParserFactory = InstanceCreator;
            return parser.Parse(Regex.Replace(expression.ToLower(), @"\s+", ""));
        }
    }
}
