using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public class ConversionMappings
    {
        private Dictionary<UnitType, double> Mappings { get; } = new Dictionary<UnitType, double>();

        public ConversionMappings(MappingsInitializer initializer)
        {
            InitializeMappings(initializer);
        }

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

    public sealed class MappingsInitializer
    {
        internal readonly List<MappingEntry> entryList = new List<MappingEntry>();

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
