using System.Collections.Generic;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Expression builder is used to convert expression elements to math component objects
    /// It must have specific structure!
    /// <para>Structure: Value - Operator - Value - ... - Value</para>
    /// <para>Value can be number / expression / function. Basically everything what helds numerical value</para>
    /// <para>Operator is.. well an operator. Like + - or ^</para>
    /// </summary>
    public class ExpressionBuilder
    {
        private readonly ConstructNewParser parserFactory;
        private readonly LinkedList<IMathComponent> components = new LinkedList<IMathComponent>();

        /// <summary>
        /// Constructs new builder instance.
        /// </summary>
        /// <param name="_parserFactory">Instance creator for <see cref="IExpressionParser"/>. Makes sure sub-expressions will be parsed by your defined <see cref="IExpressionParser"/></param>
        public ExpressionBuilder(ConstructNewParser _parserFactory)
        {
            parserFactory = _parserFactory;
        }

        /// <summary>
        /// Converts raw number to math component and inserts it into internal data structure.
        /// Internally calls <see cref="Number(double, bool)"/> with <code>false</code> negative parameter
        /// </summary>
        /// <param name="value">Number to be inserted</param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Number(double value)
        {
            return Number(value, false);
        }

        /// <summary>
        /// Converts raw number to math component and inserts it into internal data structure.
        /// </summary>
        /// <param name="value">Number to be inserted</param>
        /// <param name="negative">Whether value should be negative</param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Number(double value, bool negative)
        {
            IMathComponent component = new NumberComponent(value)
            {
                Negative = negative
            };
            components.AddLast(component);
            return this;
        }

        /// <summary>
        /// Converts Function and it's parameters to math component and inserts it into internal data structure.
        /// Internally calls <see cref="Function(Expressions.Function, string, bool)"/> with <code>false</code> negative parameter
        /// </summary>
        /// <param name="function">Function type</param>
        /// <param name="funcExpression">Function parameter expression. Can be empty string if function has no parameters. Parameters must be separated by <see cref="Expressions.Function.FUNCTION_PARAMETER_SEPARATOR"/></param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Function(Function function, string funcExpression)
        {
            return Function(function, funcExpression, false);
        }

        /// <summary>
        /// Converts Function and it's parameters to math component and inserts it into internal data structure.
        /// </summary>
        /// <param name="function">Function type</param>
        /// <param name="funcExpression">Function parameter expression. Can be empty string if function has no parameters. Parameters must be separated by <see cref="Expressions.Function.FUNCTION_PARAMETER_SEPARATOR"/></param>
        /// <param name="negative">Whether function result should be negative</param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Function(Function function, string funcExpression, bool negative)
        {
            string[] parameters;
            if (funcExpression.Length == 0)
            {
                parameters = new string[0];
            }
            else
            {
                parameters = funcExpression.Split(Expressions.Function.FUNCTION_PARAMETER_SEPARATOR);
            }
            IMathComponent component = new OperationComponent(new FunctionInstance(function, parameters.Length == 0 ? new IMathComponent[0] : ConvertToComponents(parameters)))
            {
                Negative = negative
            };
            components.AddLast(component);
            return this;
        }

        /// <summary>
        /// Converts <paramref name="operator"/> into math component and inserts it into internal data structure.
        /// </summary>
        /// <param name="operator">Operator type</param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Operator(Operator @operator)
        {
            components.AddLast(new OperatorComponent(@operator));
            return this;
        }

        /// <summary>
        /// Parses <paramref name="expression"/> into new <see cref="Expressions.Expression"/> object and inserts it into internal data structure.
        /// Internally calls <see cref="Expression(string, bool)"/> with <code>false</code> negative parameter
        /// </summary>
        /// <param name="expression">Expression in string format</param>
        /// <returns>This builder</returns>
        public ExpressionBuilder Expression(string expression)
        {
            return Expression(expression, false);
        }

        /// <summary>
        /// Parses <paramref name="expression"/> into new <see cref="Expressions.Expression"/> object and inserts it into internal data structure.
        /// </summary>
        /// <param name="expression">Expression in string format</param>
        /// <param name="negative">Whether expression result should be negative</param>
        /// <returns></returns>
        public ExpressionBuilder Expression(string expression, bool negative)
        {
            IExpressionParser parser = parserFactory.Invoke();
            parser.ParserFactory = parserFactory;
            Expression expr = parser.Parse(expression);
            expr.Negative = negative;
            components.AddLast(expr);
            return this;
        }

        /// <summary>
        /// Constructs new expression object from internal data structure.
        /// </summary>
        /// <returns>New <see cref="Expressions.Expression"/> component</returns>
        public Expression Build()
        {
            IMathComponent tree = CreateComponentTree();
            return new Expression(tree);
        }

        private IMathComponent CreateComponentTree()
        {
            GroupComponents(); // convert all elements to value types
            if (components.First == null)
                return null;
            return components.First.Value;
        }

        private void GroupComponents()
        {
            for (int i = 2; i >= 0; i--)
            {
                GroupByPriority(i);
            }
        }

        private void GroupByPriority(int priority)
        {
            if (components.First == null) return;
            var node = components.First.Next; // skipping to second component, which should be operator component
            if (node == null) return;
            while (true)
            {
                IMathComponent component = node.Value;
                if (component is OperatorComponent opComponent)
                {
                    var prevComponent = node.Previous;
                    var nextComponent = node.Next;
                    if (nextComponent == null) throw new InvalidExpressionSyntaxException("Invalid expression");
                    var nextOperator = nextComponent.Next;
                    if ((int) opComponent.Operator.ExecutionPriority == priority)
                    {
                        OperationComponent operation = new OperationComponent(new OperatorInstance(prevComponent.Value, nextComponent.Value, opComponent.Operator));
                        components.Remove(prevComponent);
                        components.Remove(nextComponent);
                        components.AddBefore(node, operation);
                        components.Remove(node);
                    }
                    if (nextOperator == null) break;
                    node = nextOperator;
                }
                else
                {
                    throw new InvalidExpressionSyntaxException("Operator expected");
                }
            }
        }

        private IMathComponent[] ConvertToComponents(string[] expressionArray)
        {
            IMathComponent[] mathComponents = new IMathComponent[expressionArray.Length];
            for (int i = 0; i < expressionArray.Length; i++)
            {
                IExpressionParser parser = parserFactory.Invoke();
                parser.ParserFactory = parserFactory;
                Expression expression = parser.Parse(expressionArray[i]);
                mathComponents[i] = expression;
            }
            return mathComponents;
        }
    }
}
