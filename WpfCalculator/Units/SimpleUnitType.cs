using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public abstract class SimpleUnitType : UnitType
    {
        public SimpleUnitType(string key) : base(key)
        {

        }

        public override Unit Parse(IDataSource source)
        {
            return new Unit(this, double.Parse(source.GetData()));
        }

        public override Unit Parse(IDataSource source, UnitType targetUnit)
        {
            throw new NotImplementedException();
        }
    }
}
