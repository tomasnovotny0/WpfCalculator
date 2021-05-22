using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    // WEIGHT MEASUREMENT
    public class KilogramUnit : SimpleUnitType
    {
        public KilogramUnit() : base("kg")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("g", 1E-3);
        }
    }

    public class GramUnit : SimpleUnitType
    {
        public GramUnit() : base("g")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("kg", 1E3);
        }
    }

    // DISTANCE MEASUREMENT
    public class MeterUnit : SimpleUnitType
    {
        public MeterUnit() : base("m")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("cm", 1E-2);
        }
    }

    public class CentimeterUnit : SimpleUnitType
    {
        public CentimeterUnit() : base("cm")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("m", 1E2);
        }
    }
}
