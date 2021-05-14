﻿using System;
using System.Windows.Media;
using WpfCalculator.Exceptions;
using WpfCalculator.Expressions;

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
                Expressions.Expression expr = parser.Parse(expression);
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
