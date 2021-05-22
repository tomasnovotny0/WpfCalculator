using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public abstract class UnitType
    {
        public ConversionMappings ConversionMappings { get; internal set; }
        public string UnitKey { get; }

        public UnitType(string unitKey)
        {
            UnitKey = unitKey;
        }

        public abstract Unit Parse(IDataSource source);

        public abstract Unit Parse(IDataSource source, UnitType targetUnit);

        public abstract void InitializeUnitMappings(MappingsInitializer initializer);

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj is UnitType type)
            {
                return type.UnitKey == UnitKey;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UnitKey.GetHashCode();
        }

        public static bool operator ==(UnitType type1, UnitType type2)
        {
            return type1.Equals(type2);
        }

        public static bool operator !=(UnitType type1, UnitType type2)
        {
            return !(type1 == type2);
        }
    }

    public static class UnitManager
    {
        private static readonly Dictionary<string, UnitType> KEY2UNIT_DICT = new Dictionary<string, UnitType>();
        // WEIGHT UNITS
        public static UnitType GRAM { get; private set; }
        public static UnitType KILOGRAM { get; private set; }

        // DISTANCE MEASUREMENT UNITS
        public static UnitType METER { get; private set; }
        public static UnitType CENTIMETER { get; private set; }
        public static UnitType FOOT { get; private set; }
        public static UnitType INCH { get; private set; }

        static UnitManager()
        {
            List<RegistryEntry> entries = new List<RegistryEntry>();
            RegisterUnits(entries);
            InitializeConversions(entries);
        }

        public static UnitType FindUnitByKey(string key)
        {
            return KEY2UNIT_DICT[key];
        }

        private static void RegisterUnits(List<RegistryEntry> entries)
        {
            GRAM = RegisterUnit(new GramUnit(), entries);
            KILOGRAM = RegisterUnit(new KilogramUnit(), entries);

            METER = RegisterUnit(new MeterUnit(), entries);
            CENTIMETER = RegisterUnit(new CentimeterUnit(), entries);
            FOOT = RegisterUnit(new FootUnit(), entries);
            INCH = RegisterUnit(new InchUnit(), entries);
        }

        private static void InitializeConversions(List<RegistryEntry> registryEntries)
        {
            foreach (RegistryEntry entry in registryEntries)
            {
                entry.Type.ConversionMappings = new ConversionMappings(entry.Initializer);
            }
        }

        private static UnitType RegisterUnit(UnitType type, List<RegistryEntry> entries)
        {
            RegistryEntry entry = new RegistryEntry(type);
            entries.Add(entry);
            KEY2UNIT_DICT[type.UnitKey] = type;
            type.InitializeUnitMappings(entry.Initializer);
            return type;
        }
    }

    internal struct RegistryEntry
    {
        internal UnitType Type { get; }
        internal MappingsInitializer Initializer { get; }

        internal RegistryEntry(UnitType type)
        {
            Type = type;
            Initializer = new MappingsInitializer();
        }
    }
}
