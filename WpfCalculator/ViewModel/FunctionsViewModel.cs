using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfCalculator.Expressions;

namespace WpfCalculator.ViewModel
{
    /// <summary>
    /// View model for functions view
    /// </summary>
    public class FunctionsViewModel : INotifyPropertyChanged
    {
        private Function _function;
        /// <summary>
        /// Currently selected function instance
        /// </summary>
        public Function SelectedFunction {
            get => _function;
            set {
                _function = value;
                OnPropertyChanged();
            } 
        }

        /// <summary>
        /// Event handler from <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes property changed event
        /// </summary>
        /// <param name="property">name of the property which was changed</param>
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
