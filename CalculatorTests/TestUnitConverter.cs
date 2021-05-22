using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Units;

namespace CalculatorTests
{
    [TestClass]
    public class TestUnitConverter
    {
        [TestMethod]
        public void TestConversion_Kg_g()
        {
            Unit kg = new(UnitManager.KILOGRAM, 1);
            Unit g = UnitConverter.ConvertUnit(kg, UnitManager.GRAM);
            Assert.AreEqual(1000, g.Value);
            Assert.AreEqual(1, UnitConverter.ConvertUnit(g, UnitManager.KILOGRAM).Value);
        }
    }
}
