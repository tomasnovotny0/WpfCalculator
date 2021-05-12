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

        private List<IMathComponent> GroupComponents()
        {
            GroupHighPriorityOperators();
            List<IMathComponent> compiled = new List<IMathComponent>();
            if (components.Count < 3)
            {
                if (components.Count != 1)
                {
                    throw new InvalidExpressionSyntaxException("Invalid syntax");
                }
                compiled.Add(components.First.Value);
                return compiled;
            }
            var root = components.First;
            var op = root.Next;
            var number2 = op.Next;
            var number1 = root.Value;

            while (true)
            {
                if (op.Value is OperatorComponent opComponent)
                {
                    OperatorInstance opInstance = new OperatorInstance(number1, number2.Value, opComponent.Operator);
                    OperationComponent operationComponent = new OperationComponent(opInstance);
                    compiled.Add(operationComponent);

                    op = number2.Next;
                    if (op == null) break;
                    number2 = op.Next;
                    // there has to be value when operator is defined
                    if (number2 == null) throw new InvalidExpressionSyntaxException("Expression expected");

                    number1 = operationComponent;
                }
                else
                {
                    throw new InvalidExpressionSyntaxException("Invalid expression");
                }
            }
            return compiled;
        }

        private void GroupHighPriorityOperators()
        {
            GroupHighPriorityOperators(2);
            GroupHighPriorityOperators(1);
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
                    if (opComponent.Operator.PriorityIndex == priority)
                    {
                        OperationComponent operation = new OperationComponent(new OperatorInstance(prevComponent.Value, nextComponent.Value, opComponent.Operator));
                        var nextOperator = nextComponent.Next;
                        components.Remove(prevComponent);
                        components.Remove(nextComponent);
                        components.AddBefore(node, operation);
                        components.Remove(node);
                        if (nextOperator == null) break;
                        node = nextOperator;
                    }
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
