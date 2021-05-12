using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public class ExpressionBuilder
    {
        private LinkedList<IMathComponent> components = new LinkedList<IMathComponent>();

        /*
         * Requirements
         * Structure
         * Value - Operation - Value - ... - Value
         * 
         * Value - Simple number component / Function component / Expression
         * Operation - Operator component
         * 
         * - Create list with structure described above
         * - Group components - Value + Operation + Value as one object
         * - Return Expression object with this structure
         */

        public ExpressionBuilder Number(double value)
        {
            components.AddLast(new NumberComponent(value));
            return this;
        }

        public ExpressionBuilder Function(Function function, string funcExpression)
        {
            string[] parameters = funcExpression.Split(',');
            IMathComponent component = new OperationComponent(new FunctionInstance(function, ConvertToComponents(parameters)));
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
            ExpressionParser parser = new ExpressionParser();
            Expression expr = parser.Parse(expression);
            components.AddLast(expr);
            return this;
        }

        public Expression Build()
        {
            return new Expression(GroupComponents());
        }

        private LinkedList<IMathComponent> GroupComponents()
        {
            GroupHighPriorityOperators(); // convert all elements to value types
            return components;
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
