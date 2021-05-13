using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfCalculator.Exceptions;
using WpfCalculator.Math;

namespace WpfCalculator
{
    class Calculator
    {
        public Brush BackgroundBrush { get => errored ? Brushes.Red : Brushes.Black; }
        public double OutputValue { get; private set; } = 0.0;
        private bool errored;

        public void Clear()
        {
            errored = false;
            OutputValue = 0;
        }

        public void UpdateValue(string expression)
        {
            errored = false;
            ExpressionParser parser = new ExpressionParser();
            try
            {
                Expression expr = parser.Parse(expression);
                OutputValue = expr.GetValue();
            }
            catch (ExpressionException)
            {
                OutputValue = 0;
                errored = true;
            }
            catch (DivideByZeroException)
            {
                OutputValue = double.NaN;
            }
        }
    }
}
