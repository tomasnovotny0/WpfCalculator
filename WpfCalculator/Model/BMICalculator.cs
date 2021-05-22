using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfCalculator.Units;

namespace WpfCalculator.Model
{
    public static class BMICalculator
    {
        public static double GetBMI(Unit height, Unit weight)
        {
            return weight.Value / (height.Value * height.Value);
        }
    }
}
