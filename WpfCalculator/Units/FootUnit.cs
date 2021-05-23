using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    /// <summary>
    /// Foot unit type.
    /// Has overriden parsing method, which allows to parse both feet and inches
    /// from one value.
    /// For example 5f9 resolves to 5ft 9in.
    /// It can also be defined normally in decimal spaces
    /// </summary>
    public class FootUnit : UnitType
    {
        public FootUnit() : base("ft", UnitCategory.LENGTH)
        {
        }

        public override Unit Parse(IDataSource source)
        {
            string data = source.GetData().ToLower();
            string[] components = data.Split('f');
            int len = components.Length;
            switch (len)
            {
                case 1:
                    return new Unit(this, double.Parse(data));
                case 2:
                    Unit inches = UnitManager.INCH.Parse(new StringDataSource(components[1]));
                    Unit feet = UnitManager.FOOT.Parse(new StringDataSource(components[0])); // calls this method again
                    return feet + inches;
                default:
                    throw new ArgumentException();
            }
        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            initializer.AddMapping("in", 1 / 12.0);
            initializer.AddMapping("m", 1 / 0.3048);
            initializer.AddMapping("cm", 1 / 30.48);
        }
    }
}
