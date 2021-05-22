using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public class FootUnitType : SimpleUnitType
    {
        public FootUnitType() : base("ft")
        {

        }

        public override Unit Parse(IDataSource source)
        {
            string data = source.GetData().ToLower();
            string[] components = data.Split('f');
            int len = components.Length;
            switch (len)
            {
                case 2:

                    break;
            }
        }

        public override void InitializeUnitMappings(MappingsInitializer initializer)
        {
            throw new NotImplementedException();
        }
    }
}
