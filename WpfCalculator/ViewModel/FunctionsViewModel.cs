using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfCalculator.Expressions;

namespace WpfCalculator.ViewModel
{
    public class FunctionsViewModel : INotifyPropertyChanged
    {
        public Function SelectedFunction { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SelectFunction(Function function)
        {
            SelectedFunction = function;
            OnPropertyChanged(nameof(SelectedFunction));
        }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
