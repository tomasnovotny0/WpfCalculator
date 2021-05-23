using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    #region Weight Units
    /// <summary>
    /// Base unit representing kilograms [kg]
    /// </summary>
    public class KilogramUnit : UnitType
    {
        public KilogramUnit() : base("kg", UnitCategory.WEIGHT)
        {
        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("g", 1E-3);
            initializer.AddMapping("lb", 2.20462262);
        }
    }
    
    /// <summary>
    /// Base unit representing grams [g]
    /// </summary>
    public class GramUnit : UnitType
    {
        public GramUnit() : base("g", UnitCategory.WEIGHT)
        {
        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("kg", 1E3);
            initializer.AddMapping("lb", 0.00220462262);
        }
    }

    /// <summary>
    /// Base unit representing pounds [lb]
    /// </summary>
    public class PoundUnit : UnitType
    {
        public PoundUnit() : base("lb", UnitCategory.WEIGHT)
        {
        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("kg", 0.45359237);
            initializer.AddMapping("g", 453.59237);
        }
    }
    #endregion

    #region Distance measurement units
    /// <summary>
    /// Base unit representing meters [m]
    /// </summary>
    public class MeterUnit : UnitType
    {
        public MeterUnit() : base("m", UnitCategory.LENGTH)
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("cm", 1E-2);
            initializer.AddMapping("ft", 0.3048);
            initializer.AddMapping("in", 0.0254);
        }
    }

    /// <summary>
    /// Base unit representing centimeters [cm]
    /// </summary>
    public class CentimeterUnit : UnitType
    {
        public CentimeterUnit() : base("cm", UnitCategory.LENGTH)
        {

        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("m", 1E2);
            initializer.AddMapping("ft", 30.48);
            initializer.AddMapping("in", 2.54);
        }
    }

    /// <summary>
    /// Base unit representing inches [in]
    /// </summary>
    public class InchUnit : UnitType
    {
        public InchUnit() : base("in", UnitCategory.LENGTH)
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
