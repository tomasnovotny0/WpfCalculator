using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Units
{
    /// <summary>
    /// Base class for all units.
    /// Contains unit identifier such as 'kg' and conversion mappings
    /// between different units like kg -> lb etc.
    /// </summary>
    public abstract class UnitType
    {
        /// <summary>
        /// Contains all conversion rates to compatible units
        /// </summary>
        public ConversionMappings ConversionMappings { get; internal set; }
        /// <summary>
        /// String identifier of this unit
        /// </summary>
        public string UnitKey { get; }
        /// <summary>
        /// Category of this unit.
        /// Useful for filtering in collections
        /// </summary>
        public UnitCategory Category { get; }

        public UnitType(string unitKey, UnitCategory category)
        {
            UnitKey = unitKey;
            Category = category;
        }

        /// <summary>
        /// Used to map conversion rates for other units
        /// </summary>
        /// <param name="initializer">Object containing all mappings of this unit</param>
        public abstract void InitializeUnitMappings(MappingsInitializer initializer);

        /// <summary>
        /// Converts specific <see cref="IDataSource"/> to <see cref="Unit"/> instance
        /// </summary>
        /// <param name="source">Data source to be parsed</param>
        /// <returns></returns>
        public virtual Unit Parse(IDataSource source)
        {
            try
            {
                return new Unit(this, double.Parse(source.GetData()));
            }
            catch (FormatException fe)
            {
                throw new InvalidDataSourceException(fe.Message);
            }
        }

        /// <summary>
        /// Converts specific <see cref="IDataSource"/> to <see cref="Unit"/> instance
        /// which is then converted to specified <see cref="UnitType"/>
        /// </summary>
        /// <param name="source">Data source to be parsed</param>
        /// <param name="targetUnit">Unit type of created <see cref="Unit"/></param>
        /// <returns></returns>
        public virtual Unit Parse(IDataSource source, UnitType targetUnit)
        {
            Unit unconverted = Parse(source);
            return UnitConverter.ConvertUnit(unconverted, targetUnit);
        }

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

    /// <summary>
    /// Contains all registered units with key mappings
    /// </summary>
    public static class UnitManager
    {
        private static readonly Dictionary<string, UnitType> KEY2UNIT_DICT = new Dictionary<string, UnitType>();
        // WEIGHT UNITS
        public static UnitType GRAM { get; private set; }
        public static UnitType KILOGRAM { get; private set; }
        public static UnitType POUND { get; private set; }

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

        /// <summary>
        /// Attempts to find unit which has specified <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key of unit you're looking for</param>
        /// <returns>Unit which has specified <paramref name="key"/></returns>
        public static UnitType FindUnitByKey(string key)
        {
            return KEY2UNIT_DICT[key];
        }

        /// <summary>
        /// Finds all units which are matching specific criteria.
        /// </summary>
        /// <param name="predicate">Criteria for unit type</param>
        /// <returns>List of all units which are matching specified <paramref name="predicate"/></returns>
        public static IList<UnitType> GetUnitTypesByCriteria(Predicate<UnitType> predicate)
        {
            IList<UnitType> list = new List<UnitType>();
            foreach (KeyValuePair<string, UnitType> pair in KEY2UNIT_DICT)
            {
                UnitType type = pair.Value;
                if (predicate.Invoke(type))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        private static void RegisterUnits(List<RegistryEntry> entries)
        {
            GRAM = RegisterUnit(new GramUnit(), entries);
            KILOGRAM = RegisterUnit(new KilogramUnit(), entries);
            POUND = RegisterUnit(new PoundUnit(), entries);

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
