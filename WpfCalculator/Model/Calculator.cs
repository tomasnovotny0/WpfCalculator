using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfCalculator
{
    class Calculator
    {
        public Brush BackgroundBrush { get => errored ? Brushes.Red : Brushes.Black; }
        public double Result { get; private set; } = 0;
        private bool errored;

        public void UpdateValue(string textInput)
        {
            errored = false;
            if (textInput == null || textInput.Length == 0)
            {
                return;
            }
        }
    }
}
