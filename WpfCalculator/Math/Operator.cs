using System;
using System.Collections.Generic;

namespace WpfCalculator.Math
{
    public sealed class Operator
    {
        public char OperatorCharacter { get; }
        public Func<IMathComponent, IMathComponent, double> OperatorFunction { get; }

        internal Operator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func)
        {
            OperatorCharacter = operatorCharacter;
            OperatorFunction = func;
        }

        public double Combine(IMathComponent input1, IMathComponent input2)
        {
            return OperatorFunction.Invoke(input1, input2);
        }
    }

    public static class Operators
    {
        private static List<Operator> operators = new List<Operator>();

        public static readonly Operator ADD = RegisterOperator('+', (a, b) => a.GetValue() + b.GetValue());
        public static readonly Operator SUBTRACT = RegisterOperator('-', (a, b) => a.GetValue() - b.GetValue());
        public static readonly Operator MULTIPLY = RegisterOperator('*', (a, b) => a.GetValue() * b.GetValue());
        public static readonly Operator DIVIDE = RegisterOperator('/', (a, b) =>
        {
            double dividingBy = b.GetValue();
            if (dividingBy == 0) throw new DivideByZeroException();
            return a.GetValue() / dividingBy;
        });

        public static Operator RegisterOperator(char operatorCharacter, Func<IMathComponent, IMathComponent, double> func)
        {
            Operator @operator = new Operator(operatorCharacter, func);
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
