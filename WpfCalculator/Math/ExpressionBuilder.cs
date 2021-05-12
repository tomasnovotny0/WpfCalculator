using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
            // TODO first group operators with higher priorities
            List<IMathComponent> compiled = new List<IMathComponent>();
            if (components.Count < 3)
                // TODO try parse single value type in case user enters uncommon expression
                throw new InvalidExpressionSyntaxException("Invalid syntax");
            var root = components.First;
            var op = root.Next;
            var number2 = op.Next;
            var number1 = root.Value;

            bool valid = true;
            while (valid)
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
                    valid = false;
                }
            }
            return compiled;
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
