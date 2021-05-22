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

        public static bool operator ==(Unit op1, Unit op2) => op1.UnitType == op2.UnitType && op1.Value == op2.Value;

        public static bool operator !=(Unit op1, Unit op2) => !(op1 == op2);

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is Unit unit)
            {
                return this == unit;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() << 31 | UnitType.GetHashCode();
        }

        private static Unit Combine(Unit op1, Unit op2, Func<double, double, double> func)
        {
            UnitType target = op1.UnitType;
            Unit converted = UnitConverter.ConvertUnit(op2, target);
            return new Unit(target, func.Invoke(op1.Value, converted.Value));
        }
    }
}
