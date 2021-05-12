using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator
{
    class MainViewModel : INotifyPropertyChanged
    {
        public Calculator Calculator { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Calculator = new Calculator();

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        }

        public void ParseCalculatorInput(string input)
        {
            // 15+sqrt(12+12)+14*15+(1*2/3)^2
            if (input != null)
            {
                Calculator.UpdateValue(input);
                OnPropertyChanged(nameof(Calculator));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
