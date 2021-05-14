using System;
using System.Collections.Generic;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    public sealed class Operator
    {
        public char OperatorCharacter { get; }
        public Func<IMathComponent, IMathComponent, double> OperatorFunction { get; }
        public ExecutionPriority ExecutionPriority { get; }

        internal Operator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, ExecutionPriority executionPriority)
        {
            OperatorCharacter = operatorCharacter;
            OperatorFunction = func;
            ExecutionPriority = executionPriority;
        }

        public double Combine(IMathComponent input1, IMathComponent input2)
        {
            return OperatorFunction.Invoke(input1, input2);
        }
    }

    public static class Operators
    {
        private static List<Operator> operators = new List<Operator>();

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

        public static Operator RegisterOperator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, ExecutionPriority executionPriority)
        {
            Operator @operator = new Operator(operatorCharacter, func, executionPriority);
            operators.Add(@operator);
            return @operator;
        }

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

    public enum ExecutionPriority
    {
        LOW, MEDIUM, HIGH
    }
}
