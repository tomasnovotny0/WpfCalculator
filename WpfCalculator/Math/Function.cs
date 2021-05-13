using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Math
{
    public sealed class Function
    {
        public int InputCount { get; private set; }
        private Func<IMathComponent[], double> func;
        private string functionName;

        private Function(int inputAmount, Func<IMathComponent[], double> func, string functionName)
        {
            InputCount = inputAmount;
            this.func = func;
            this.functionName = functionName.ToLower();
        }

        public double Calculate(IMathComponent[] parameters)
        {
            return func.Invoke(parameters);
        } 

        public static Function CreateFunction(string functionName, int parameterAmount, Func<IMathComponent[], double> computeFunc)
        {
            if (parameterAmount < 0)
                throw new ArgumentOutOfRangeException();
            if (computeFunc == null)
                throw new NullReferenceException();
            return new Function(parameterAmount, computeFunc, functionName);
        }

        public override string ToString()
        {
            return functionName;
        }
    }

    public static class Functions
    {
        private static List<Function> functions = new List<Function>();

        public static readonly Function SQRT = RegisterFunction("sqrt", 1, args => System.Math.Sqrt(args[0].GetValue()));
        public static readonly Function SQRTX = RegisterFunction("sqrtx", 2, args => System.Math.Pow(args[1].GetValue(), 1.0 / args[0].GetValue()));
        public static readonly Function SIN = RegisterFunction("sin", 1, args => System.Math.Sin(args[0].GetValue()));
        public static readonly Function COS = RegisterFunction("cos", 1, args => System.Math.Cos(args[0].GetValue()));
        public static readonly Function TAN = RegisterFunction("tan", 1, args => System.Math.Tan(args[0].GetValue()));
        public static readonly Function PI = RegisterFunction("pi", 0, args => System.Math.PI);

        public static Function RegisterFunction(string funcKey, int parameterCount, Func<IMathComponent[], double> computeFunction)
        {
            Function function = Function.CreateFunction(funcKey, parameterCount, computeFunction);
            functions.Add(function);
            return function;
        }

        public static Function FindFunction(string functionName)
        {
            foreach (Function function in functions)
            {
                if (function.ToString() == functionName)
                {
                    return function;
                }
            }
            throw new UnknownFunctionException(functionName);
        }
    }
}
