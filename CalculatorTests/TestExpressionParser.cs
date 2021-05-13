using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using WpfCalculator.Exceptions;
using WpfCalculator.Math;

namespace CalculatorTests
{
    [TestClass]
    public class TestExpressionParser
    {
        private ExpressionParser parser;

        [TestInitialize]
        public void TestInit()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
            parser = new ExpressionParser();
        }

        [TestMethod]
        public void TestSubExpressions()
        {
            string expr = "(15+3)";
            int readIndex = 1;
            Assert.AreEqual("15+3", ExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestBiggerSubExpression()
        {
            string expr = "(1+(15*(5-2)))";
            int readIndex = 1;
            Assert.AreEqual("1+(15*(5-2))", ExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestSubExpressionInvalid()
        {
            string expr = "((1+1)*7";
            int readIndex = 1;
            Assert.ThrowsException<InvalidExpressionSyntaxException>(() => ExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestSimpleExpression()
        {
            string expressionString = "1+5-3";
            ExpressionParser parser = new ExpressionParser();
            Expression expression = parser.Parse(expressionString);
            Assert.AreEqual(3.0, expression.GetValue(), 0.0001);
        }
    }
}
