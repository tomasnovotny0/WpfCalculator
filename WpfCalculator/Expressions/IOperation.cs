namespace WpfCalculator.Expressions
{
    public interface IOperation
    {
        /// <summary>
        /// Performs specific operation and returns result
        /// </summary>
        /// <returns>Result of specific operation</returns>
        double Calculate();
    }
}
