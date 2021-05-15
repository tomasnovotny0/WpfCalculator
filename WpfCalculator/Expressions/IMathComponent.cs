namespace WpfCalculator.Expressions
{
    public interface IMathComponent
    {
        /// <summary>
        /// Property determining whether value should be negative or not
        /// </summary>
        bool Negative { get; set; }

        /// <summary>
        /// Method for getting value from this component
        /// </summary>
        /// <returns>Value held inside this component</returns>
        double GetValue();
    }
}
