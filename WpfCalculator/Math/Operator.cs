using System;
using System.Collections.Generic;

namespace WpfCalculator.Math
{
    public sealed class Operator
    {
        public char OperatorCharacter { get; }
        public Func<IMathComponent, IMathComponent, double> OperatorFunction { get; }
        public int PriorityIndex { get; }

        internal Operator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, int priorityIndex)
        {
            OperatorCharacter = operatorCharacter;
            OperatorFunction = func;
            PriorityIndex = priorityIndex;
        }

        public double Combine(IMathComponent input1, IMathComponent input2)
        {
            return OperatorFunction.Invoke(input1, input2);
        }
    }

    public static class Operators
    {
        private static List<Operator> operators = new List<Operator>();

        public static readonly Operator ADD = RegisterOperator('+', (a, b) => a.GetValue() + b.GetValue(), 0);
        public static readonly Operator SUBTRACT = RegisterOperator('-', (a, b) => a.GetValue() - b.GetValue(), 0);
        public static readonly Operator MULTIPLY = RegisterOperator('*', (a, b) => a.GetValue() * b.GetValue(), 1);
        public static readonly Operator DIVIDE = RegisterOperator('/', (a, b) =>
        {
            double dividingBy = b.GetValue();
            if (dividingBy == 0) throw new DivideByZeroException();
            return a.GetValue() / dividingBy;
        }, 1);
        public static readonly Operator POWER = RegisterOperator('^', (a, b) => System.Math.Pow(a.GetValue(), b.GetValue()), 2);
        public static readonly Operator MODULO = RegisterOperator('%', (a, b) => a.GetValue() % b.GetValue(), 1);

        public static Operator RegisterOperator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func, int priorityIndex)
        {
            Operator @operator = new Operator(operatorCharacter, func, priorityIndex);
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
            return null;
        }
    }
}
