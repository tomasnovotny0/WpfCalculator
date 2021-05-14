namespace WpfCalculator.Expressions
{
    public class NumberComponent : IMathComponent
    {
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
