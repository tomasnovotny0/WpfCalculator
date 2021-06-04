using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using WpfCalculator.Exceptions;
using WpfCalculator.Expressions;

namespace WpfCalculator
{
    /// <summary>
    /// Contains <see cref="Expressions.ExpressionProcessor"/> for processing various expressions.
    /// Also contains properties which are bound to window elements
    /// </summary>
    class Calculator : INotifyPropertyChanged
    {
        /// <summary>
        /// Brush which sets foreground color of output label in calculator window.
        /// Attached via binding
        /// </summary>
        public Brush TextColorBrush { get => errored ? Brushes.Red : Brushes.Black; }
        /// <summary>
        /// Numerical value of last processed expression.
        /// Attached via binding
        /// </summary>
        public double OutputValue
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Processor used to process expressions.
        /// Uses <see cref="SimpleExpressionParser"/> implementation
        /// </summary>
        public ExpressionProcessor ExpressionProcessor { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private double _result;
        private bool errored;

        public Calculator()
        {
            ExpressionProcessor = new ExpressionProcessor(() => new SimpleExpressionParser());
        }

        /// <summary>
        /// Resets values after last expression processing
        /// </summary>
        public void Clear()
        {
            errored = false;
            OutputValue = 0;
        }

        /// <summary>
        /// Parses and updates value based on <paramref name="expression"/>
        /// </summary>
        /// <param name="expression">Expression to be processed</param>
        public void UpdateValue(string expression)
        {
            errored = false;
            try
            {
                Expression expr = ExpressionProcessor.ProcessExpression(expression);
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

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
