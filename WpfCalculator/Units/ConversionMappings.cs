using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    /// <summary>
    /// Class containing conversion mappings to compatible unit types
    /// </summary>
    public class ConversionMappings
    {
        /// <summary>
        /// Dictionary with mappings
        /// </summary>
        private Dictionary<UnitType, double> Mappings { get; } = new Dictionary<UnitType, double>();

        public ConversionMappings(MappingsInitializer initializer)
        {
            InitializeMappings(initializer);
        }

        /// <summary>
        /// Get conversion rate for specific unit <paramref name="type"/>
        /// </summary>
        /// <param name="type">Unit type from which we're getting it's conversion rate</param>
        /// <returns>Conversion rate for specified <paramref name="type"/></returns>
        public double GetConversionValueForUnit(UnitType type)
        {
            return Mappings[type];
        }

        private void InitializeMappings(MappingsInitializer initializer)
        {
            foreach (MappingEntry entry in initializer.entryList)
            {
                UnitType type = UnitManager.FindUnitByKey(entry.key);
                Mappings[type] = entry.value;
            }
        }
    }

    /// <summary>
    /// Class which contains mapping entries which will be then
    /// converted to <see cref="ConversionMappings"/> once
    /// all unit types are initialized
    /// </summary>
    public sealed class MappingsInitializer
    {
        internal readonly List<MappingEntry> entryList = new List<MappingEntry>();

        /// <summary>
        /// Adds mapping for specific unit
        /// </summary>
        /// <param name="unitKey">Unit key which is being mapped to this unit</param>
        /// <param name="valueForUnit">Value of unit when converted to this type. For example use 0.01 when mapping 'cm' to 'm'</param>
        public void AddMapping(string unitKey, double valueForUnit)
        {
            entryList.Add(new MappingEntry(unitKey, valueForUnit));
        }
    }

    internal struct MappingEntry
    {
        internal string key;
        internal double value;

        internal MappingEntry(string _key, double _value)
        {
            key = _key;
            value = _value;
        }
    }
}
