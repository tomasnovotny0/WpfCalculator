using System;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public interface IExpressionParser
    {
        /// <summary>
        /// Property containing <see cref="ConstructNewParser"/> delegate which can be passed 
        /// to <see cref="ExpressionBuilder"/> to make sure sub-expressions are parsed by the same parser type
        /// </summary>
        ConstructNewParser ParserFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">Expression to be parsed</param>
        /// <returns>
        /// <para>Expression object or throws an exception</para>
        /// <para>Throws <see cref="ExpressionException"/> when expression is invalid</para>
        /// Throws <see cref="DivideByZeroException"/> when division by 0 occurs inside expression
        /// </returns>
        Expression Parse(string expression);
    }
}
