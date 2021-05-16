using System;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public interface IExpressionParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">Expression to be parsed</param>
        /// <param name="parserInstanceCreator">Handles creating parser instances for sub-expression parsing</param>
        /// <returns>
        /// <para>Expression object or throws an exception</para>
        /// <para>Throws <see cref="ExpressionException"/> when expression is invalid</para>
        /// Throws <see cref="DivideByZeroException"/> when division by 0 occurs inside expression
        /// </returns>
        Expression Parse(string expression, ConstructNewParser parserInstanceCreator);
    }
}
