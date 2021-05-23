using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Units;

namespace WpfCalculator.Exceptions
{
    public class InvalidDataSourceException : Exception
    {
        public InvalidDataSourceException(string message) : base($"Invalid data source: {message}")
        {

        }
    }
}
