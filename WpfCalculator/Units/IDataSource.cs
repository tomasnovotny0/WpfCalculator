using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Units
{
    /// <summary>
    /// Contains some kind of data.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets data from this data source.
        /// </summary>
        /// <returns>Data as string</returns>
        string GetData();
    }

    /// <summary>
    /// Contains data in string format.
    /// </summary>
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
