using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

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
            if (input != null)
            {
                if (input.Length == 0)
                {
                    Calculator.Clear();
                } 
                else
                {
                    Calculator.UpdateValue(input);
                }
                OnPropertyChanged(nameof(Calculator));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
