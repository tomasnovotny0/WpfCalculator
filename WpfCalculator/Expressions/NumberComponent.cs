namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Math component containing raw number
    /// </summary>
    public class NumberComponent : IMathComponent
    {
        /// <summary>
        /// Numerical value of this component
        /// </summary>
        public double Value { get; }
        public bool Negative { get; set; }

        public NumberComponent(double value)
        {
            Value = value;
        }
        
        public double GetValue()
        {
            return Negative ? -Value : Value;
        }
    }
}
