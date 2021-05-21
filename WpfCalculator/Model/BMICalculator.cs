using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfCalculator.Model
{
    public static class BMICalculator
    {
        public static double GetBMI(double height, double weight)
        {
            return weight / (height * height);
        }
    }
}
