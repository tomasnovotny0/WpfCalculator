using System;
using System.Collections.Generic;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Operator represent operation between two math components. Typical mathematical operators are +, - and so on.
    /// </summary>
    public sealed class Operator
    {
        /// <summary>
        /// Character unique for this operator
        /// </summary>
        public char OperatorCharacter { get; }
        /// <summary>
        /// Function which applies this operator on two math components and returns number
        /// </summary>
        public Func<IMathComponent, IMathComponent, double> OperatorFunction { get; }
        /// <summary>
        /// Priority of this operator. For example multiplication has higher priority than addition or substraction
        /// </summary>
        public ExecutionPriority ExecutionPriority { get; }

        internal Operator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, ExecutionPriority executionPriority)
        {
            OperatorCharacter = operatorCharacter;
            OperatorFunction = func;
            ExecutionPriority = executionPriority;
        }

        /// <summary>
        /// Combines two math component and return number
        /// </summary>
        /// <param name="input1">Math component on left side of this operator</param>
        /// <param name="input2">Math component of right side of this operator</param>
        /// <returns>Result of this operator's operation</returns>
        public double Combine(IMathComponent input1, IMathComponent input2)
        {
            return OperatorFunction.Invoke(input1, input2);
        }
    }

    /// <summary>
    /// Static class containing common operators
    /// </summary>
    public static class Operators
    {
        private static readonly List<Operator> operators = new List<Operator>();

        public static readonly Operator ADD = RegisterOperator('+', (a, b) => a.GetValue() + b.GetValue(), ExecutionPriority.LOW);
        public static readonly Operator SUBTRACT = RegisterOperator('-', (a, b) => a.GetValue() - b.GetValue(), ExecutionPriority.LOW);
        public static readonly Operator MULTIPLY = RegisterOperator('*', (a, b) => a.GetValue() * b.GetValue(), ExecutionPriority.MEDIUM);
        public static readonly Operator DIVIDE = RegisterOperator('/', (a, b) =>
        {
            double dividingBy = b.GetValue();
            if (dividingBy == 0) throw new DivideByZeroException();
            return a.GetValue() / dividingBy;
        }, ExecutionPriority.MEDIUM);
        public static readonly Operator POWER = RegisterOperator('^', (a, b) => System.Math.Pow(a.GetValue(), b.GetValue()), ExecutionPriority.HIGH);
        public static readonly Operator MODULO = RegisterOperator('%', (a, b) => a.GetValue() % b.GetValue(), ExecutionPriority.MEDIUM);

        /// <summary>
        /// Registers operator for use in calculator
        /// </summary>
        /// <param name="operatorCharacter">Unique character for this operator</param>
        /// <param name="func">Mathematical operation performed on two math components</param>
        /// <param name="executionPriority">Execution priority of this operator</param>
        /// <returns>New operator instance</returns>
        public static Operator RegisterOperator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, ExecutionPriority executionPriority)
        {
            Operator @operator = new Operator(operatorCharacter, func, executionPriority);
            operators.Add(@operator);
            return @operator;
        }

        /// <summary>
        /// Finds operator by it's unique character
        /// </summary>
        /// <param name="operatorCharacter">Character of operator you're looking for</param>
        /// <returns>Operator with <paramref name="operatorCharacter"/> unique character or throws exception when no such operator exists</returns>
        public static Operator FindOperator(char operatorCharacter)
        {
            foreach (Operator op in operators)
            {
                if (op.OperatorCharacter == operatorCharacter)
                    return op;
            }
            throw new UnknownOperatorException(operatorCharacter);
        }
    }

    /// <summary>
    /// Enum type containing execution priorities used by operators
    /// </summary>
    public enum ExecutionPriority
    {
        LOW, MEDIUM, HIGH
    }
}
