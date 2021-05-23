using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    /// <summary>
    /// Handles unit conversions.
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Converts unit to other unit
        /// </summary>
        /// <param name="unit">Unit which is being converted</param>
        /// <param name="targetType">Result type of <paramref name="unit"/></param>
        /// <returns>Unit with converted type and value or <paramref name="unit"/> when types are equal</returns>
        public static Unit ConvertUnit(Unit unit, UnitType targetType)
        {
            if (unit.UnitType == targetType)
            {
                return unit;
            }
            UnitType convertFrom = unit.UnitType;
            ConversionMappings mappings = targetType.ConversionMappings;
            double rate = mappings.GetConversionValueForUnit(convertFrom);
            return new Unit(targetType, unit.Value * rate);
        }

        /// <summary>
        /// Gets unit conversion rate
        /// </summary>
        /// <param name="convertFrom">Type which will be converted</param>
        /// <param name="convertTo">Result type</param>
        /// <returns>Conversion rate between two units or 1 when unit types are equal</returns>
        public static double GetConversionRate(UnitType convertFrom, UnitType convertTo)
        {
            if (convertFrom == convertTo)
            {
                return 1;
            }
            ConversionMappings mappings = convertTo.ConversionMappings;
            return mappings.GetConversionValueForUnit(convertFrom);
        }
    }
}
