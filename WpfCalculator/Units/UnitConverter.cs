using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public static class UnitConverter
    {
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
