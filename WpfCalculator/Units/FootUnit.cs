using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public class FootUnit : SimpleUnitType
    {
        public FootUnit() : base("ft")
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
