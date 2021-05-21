using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WpfCalculator
{
    /// <summary>
    /// View model for main application window
    /// </summary>
    class StandartCalculatorViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Calculator instance used by app
        /// </summary>
        public Calculator Calculator { get; set; }
        /// <summary>
        /// Event handler from <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public StandartCalculatorViewModel()
        {
            Calculator = new Calculator();
            // set application culture to 'en' as that has . character as decimal separator
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        }

        /// <summary>
        /// Parses specified expression
        /// </summary>
        /// <param name="input">Expression which should be parsed</param>
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
