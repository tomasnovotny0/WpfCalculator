using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfCalculator.Units;

namespace WpfCalculator.Model
{
    /// <summary>
    /// BMI calculator class
    /// </summary>
    public static class BMICalculator
    {
        /// <summary>
        /// Get BMI based on height and weight.
        /// Input units are converted to base types.
        /// Result is based on this expression:
        /// <para>BMI = weight / height^2</para>
        /// </summary>
        /// <param name="height">Height unit</param>
        /// <param name="weight">Weight unit</param>
        /// <returns>Body mass index based on height and weight</returns>
        public static double GetBMI(Unit height, Unit weight)
        {
            Unit weightKg = UnitConverter.ConvertUnit(weight, UnitManager.KILOGRAM);
            Unit heightM = UnitConverter.ConvertUnit(height, UnitManager.METER);
            return weightKg.Value / (heightM.Value * heightM.Value);
        }
    }
}
