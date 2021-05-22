using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public struct Unit
    {
        public UnitType UnitType { get; }
        public double Value { get; }

        public Unit(UnitType unitType, double value)
        {
            UnitType = unitType;
            Value = value;
        }

        public static Unit operator +(Unit op1, Unit op2) => Combine(op1, op2, (a, b) => a + b);

        public static Unit operator -(Unit op1, Unit op2) => Combine(op1, op2, (a, b) => a - b);

        public static Unit operator *(Unit op1, Unit op2) => Combine(op1, op2, (a, b) => a * b);

        public static Unit operator /(Unit op1, Unit op2) => Combine(op1, op2, (a, b) => a / b);

        private static Unit Combine(Unit op1, Unit op2, Func<double, double, double> func)
        {
            UnitType target = op1.UnitType;
            Unit converted = UnitConverter.ConvertUnit(op2, target);
            return new Unit(target, func.Invoke(op1.Value, converted.Value));
        }
    }
}
