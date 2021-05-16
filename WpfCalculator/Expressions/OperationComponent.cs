namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Math component containing specific <see cref="IOperation"/>
    /// </summary>
    public class OperationComponent : IMathComponent
    {
        public bool Negative { get; set; }
        /// <summary>
        /// Numeric operation of this component
        /// </summary>
        public IOperation Operation { get; }

        public OperationComponent(IOperation operation)
        {
            Operation = operation;
        }

        public double GetValue()
        {
            return Negative ? -Operation.Calculate() : Operation.Calculate();
        }
    }
}
