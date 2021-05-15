using System;
using System.Windows.Media;
using WpfCalculator.Exceptions;
using WpfCalculator.Expressions;

namespace WpfCalculator
{
    class Calculator
    {
        public Brush BackgroundBrush { get => errored ? Brushes.Red : Brushes.Black; }
        public double OutputValue { get; private set; } = 0.0;
        public ExpressionProcessor ExpressionProcessor { get; }
        private bool errored;

        public Calculator()
        {
            ExpressionProcessor = new ExpressionProcessor(() => new SimpleExpressionParser());
        }

        public void Clear()
        {
            errored = false;
            OutputValue = 0;
        }

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
    }
}
