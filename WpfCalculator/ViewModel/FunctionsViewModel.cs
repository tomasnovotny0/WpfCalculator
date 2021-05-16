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
        /// <summary>
        /// Currently selected function instance
        /// </summary>
        public Function SelectedFunction { get; private set; }

        /// <summary>
        /// Event handler from <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Selects specific <paramref name="function"/> and invokes property changed event
        /// </summary>
        /// <param name="function">Function which has been selected</param>
        public void SelectFunction(Function function)
        {
            SelectedFunction = function;
            OnPropertyChanged(nameof(SelectedFunction));
        }

        /// <summary>
        /// Invokes property changed event
        /// </summary>
        /// <param name="property">name of the property which was changed</param>
        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
