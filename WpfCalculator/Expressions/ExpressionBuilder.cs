using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public class ExpressionBuilder
    {
        private LinkedList<IMathComponent> components = new LinkedList<IMathComponent>();

        public ExpressionBuilder Number(double value)
        {
            return Number(value, false);
        }

        public ExpressionBuilder Number(double value, bool negative)
        {
            IMathComponent component = new NumberComponent(value)
            {
                Negative = negative
            };
            components.AddLast(component);
            return this;
        }

        public ExpressionBuilder Function(Function function, string funcExpression)
        {
            return Function(function, funcExpression, false);
        }

        public ExpressionBuilder Function(Function function, string funcExpression, bool negative)
        {
            string[] parameters;
            if (funcExpression.Length == 0)
            {
                parameters = new string[0];
            }
            else
            {
                parameters = funcExpression.Split(ExpressionParser.FUNCTION_PARAMETER_SEPARATOR);
            }
            IMathComponent component = new OperationComponent(new FunctionInstance(function, parameters.Length == 0 ? new IMathComponent[0] : ConvertToComponents(parameters)))
            {
                Negative = negative
            };
            components.AddLast(component);
            return this;
        }

        public ExpressionBuilder Operator(Operator @operator)
        {
            components.AddLast(new OperatorComponent(@operator));
            return this;
        }

        public ExpressionBuilder Expression(string expression)
        {
            return Expression(expression, false);
        }

        public ExpressionBuilder Expression(string expression, bool negative)
        {
            ExpressionParser parser = new ExpressionParser();
            Expression expr = parser.Parse(expression);
            expr.Negative = negative;
            components.AddLast(expr);
            return this;
        }

        public Expression Build()
        {
            IMathComponent tree = CreateComponentTree();
            return new Expression(tree);
        }

        private IMathComponent CreateComponentTree()
        {
            GroupHighPriorityOperators(); // convert all elements to value types
            if (components.First == null)
                return null;
            return components.First.Value;
        }

        private void GroupHighPriorityOperators()
        {
            for (int i = 2; i >= 0; i--)
            {
                GroupHighPriorityOperators(i);
            }
        }

        private void GroupHighPriorityOperators(int priority)
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
                ExpressionParser parser = new ExpressionParser();
                Expression expression = parser.Parse(expressionArray[i]);
                mathComponents[i] = expression;
            }
            return mathComponents;
        }
    }
}
