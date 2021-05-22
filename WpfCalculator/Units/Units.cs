using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    #region Weight Units
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
    #endregion

    #region Distance measurement units
    public class MeterUnit : SimpleUnitType
    {
        public MeterUnit() : base("m")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("cm", 1E-2);
            initializer.AddMapping("ft", 0.3048);
            initializer.AddMapping("in", 0.0254);
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
            initializer.AddMapping("ft", 30.48);
            initializer.AddMapping("in", 2.54);
        }
    }

    public class InchUnit : SimpleUnitType
    {
        public InchUnit() : base("in")
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("ft", 12);
            initializer.AddMapping("cm", 1 / 2.54);
            initializer.AddMapping("m", 1 / 0.0254);
        }
    }
    #endregion
}
