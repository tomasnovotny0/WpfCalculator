using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;
using WpfCalculator.Math;

namespace CalculatorTests
{
    [TestClass]
    public class TextExpressionBuilder
    {
        private ExpressionBuilder builder;

        [TestInitialize]
        public void TestInit()
        {
            builder = new ExpressionBuilder();
        }

        [TestMethod]
        public void TestAdd()
        {
            Expression expression = builder.Number(15).Operator(Operators.ADD).Number(5).Build();
            Assert.AreEqual(20.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestSubtract()
        {
            Expression expression = builder.Number(12.35).Operator(Operators.SUBTRACT).Number(12).Operator(Operators.SUBTRACT).Number(0.35).Build();
            Assert.AreEqual(0.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestMultiply()
        {
            Expression expression = builder.Number(4).Operator(Operators.MULTIPLY).Number(5).Operator(Operators.MULTIPLY).Number(5).Build();
            Assert.AreEqual(100.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestDivide()
        {
            Expression expression = builder.Number(25).Operator(Operators.DIVIDE).Number(5).Build();
            Assert.AreEqual(5.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestPower()
        {
            Expression expression = builder.Number(2).Operator(Operators.POWER).Number(3).Build();
            Assert.AreEqual(8.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestModulo()
        {
            Expression expression = builder.Number(8).Operator(Operators.MODULO).Number(3).Build();
            Assert.AreEqual(2.0, expression.GetValue(), 0.0001);
        }

        [TestMethod]
        public void TestCorrectExecution()
        {
            // 15 + 3*5 - 4^2 * 4 / 8 % 3 + 1 = 29
            Expression expression = builder.Number(15).Operator(Operators.ADD).Number(3).Operator(Operators.MULTIPLY).Number(5).Operator(Operators.SUBTRACT).Number(4)
                .Operator(Operators.POWER).Number(2).Operator(Operators.MULTIPLY).Number(4).Operator(Operators.DIVIDE).Number(8).Operator(Operators.MODULO).Number(3)
                .Operator(Operators.ADD).Number(1).Build();
            Assert.AreEqual(29.0, expression.GetValue(), 0.001);
        }

        [TestMethod]
        public void TryInvalidInput()
        {
            Assert.ThrowsException<InvalidExpressionSyntaxException>(() => builder.Number(-15).Operator(Operators.DIVIDE).Build());
        }

        [TestMethod]
        public void TryDivideByZero()
        {
            Expression expression = builder.Number(0).Operator(Operators.DIVIDE).Number(0).Build();
            Assert.ThrowsException<DivideByZeroException>(() => expression.GetValue());
        }

        [TestMethod]
        public void TestPi()
        {
            Expression expression = builder.Function(Functions.PI, "").Build();
            Assert.AreEqual(System.Math.PI, expression.GetValue(), 0.0001);
        }
    }
}
