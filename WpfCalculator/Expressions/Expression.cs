namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Represents some kind of mathematical expression
    /// which is held in <see cref="Expression.componentTree"/> object.
    /// </summary>
    public class Expression : IMathComponent
    {
        public bool Negative { get; set; }
        private readonly IMathComponent componentTree;

        public Expression(IMathComponent componentTree)
        {
            this.componentTree = componentTree;
        }

        public double GetValue()
        {
            return componentTree == null ? 0 : Negative ? -componentTree.GetValue() : componentTree.GetValue();
        }
    }
}
