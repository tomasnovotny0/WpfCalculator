using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using WpfCalculator.Exceptions;
using WpfCalculator.Expressions;

namespace CalculatorTests
{
    [TestClass]
    public class TestExpressionParser
    {
        private ExpressionProcessor processor;

        [TestInitialize]
        public void TestInit()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
            processor = new ExpressionProcessor(() => new SimpleExpressionParser());
        }

        [TestMethod]
        public void TestSubExpressions()
        {
            string expr = "(15+3)";
            int readIndex = 1;
            Assert.AreEqual("15+3", SimpleExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestBiggerSubExpression()
        {
            string expr = "(1+(15*(5-2)))";
            int readIndex = 1;
            Assert.AreEqual("1+(15*(5-2))", SimpleExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestSubExpressionInvalid()
        {
            string expr = "((1+1)*7";
            int readIndex = 1;
            Assert.ThrowsException<InvalidExpressionSyntaxException>(() => SimpleExpressionParser.GetExpression(expr, ref readIndex));
        }

        [TestMethod]
        public void TestSimpleExpression()
        {
            string expressionString = "1+5-3";
            Expression expression = processor.ProcessExpression(expressionString);
            Assert.AreEqual(3.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestComplicatedExpression()
        {
            string expressionString = "16+sqrtx(3,64)+sqrt(4)*2^2";
            Expression expression = processor.ProcessExpression(expressionString);
            Assert.AreEqual(28.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestNoParameterFunctionAndOperator()
        {
            string expressionString = "pi+";
            Assert.ThrowsException<InvalidExpressionSyntaxException>(() => processor.ProcessExpression(expressionString));
        }

        [TestMethod]
        public void TestNegativeValuesInExpressions()
        {
            string expressionString = "-11-(-sqrt(121))";
            Expression expression = processor.ProcessExpression(expressionString);
            Assert.AreEqual(0.0, expression.GetValue());
        }
    }
}
