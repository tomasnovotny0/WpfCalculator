using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.functionName = functionName;
        }

        public double Calculate(IMathComponent[] parameters)
        {
            return func.Invoke(parameters);
        } 

        public static Function CreateFunction(string functionName, int parameterAmount, Func<IMathComponent[], double> computeFunc)
        {
            if (parameterAmount < 1)
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

        public static Function SQRT { get; } = RegisterFunction("sqrt", 1, args => System.Math.Sqrt(args[0].GetValue()));

        public static Function RegisterFunction(string funcKey, int parameterCount, Func<IMathComponent[], double> computeFunction)
        {
            Function function = Function.CreateFunction(funcKey, parameterCount, computeFunction);
            functions.Add(function);
            return function;
        }
    }
}
