using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    public interface IDataSource
    {
        string GetData();
    }

    public struct StringDataSource : IDataSource
    {
        public string Data { get; }

        public StringDataSource(string data)
        {
            Data = data;
        }

        public string GetData()
        {
            return Data;
        }
    }
}
