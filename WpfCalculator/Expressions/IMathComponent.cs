namespace WpfCalculator.Expressions
{
    public interface IMathComponent
    {
        bool Negative { get; set; }

        double GetValue();
    }
}
