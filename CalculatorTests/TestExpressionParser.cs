using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
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
        public void TestNumberParsing()
        {
            int readIndex;
            Assert.AreEqual(15.235, parser.StartReading("15.235", out readIndex));
            readIndex = 0;
            Assert.AreEqual(-115.8976, parser.StartReading("-115.8976", out readIndex));
        }
    }
}
