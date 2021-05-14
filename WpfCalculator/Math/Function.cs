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
        public string FunctionName { get; }
        public string FunctionSignature { get; }
        public string DescriptionText { get; }
        private Func<IMathComponent[], double> Func { get; }

        internal Function(FunctionBuilder builder)
        {
            InputCount = builder.arguments.Count;
            FunctionName = builder.key;
            Func = builder.func;
            DescriptionText = CreateDescriptionString(builder.genericDescription, builder.arguments);
            FunctionSignature = CreateFunctionSignature(builder.arguments);
        }

        public double Calculate(IMathComponent[] parameters)
        {
            return Func.Invoke(parameters);
        }

        public override string ToString()
        {
            return FunctionName;
        }

        private string CreateDescriptionString(string generic, List<FunctionArgument> arguments)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(generic);
            if (arguments.Count > 0)
            {
                builder.Append("\n").Append("Arguments:\n");
                int lastIndex = arguments.Count - 1;
                for (int i = 0; i < arguments.Count; i++)
                {
                    builder.Append(arguments[i].GetFullDescription());
                    if (i != lastIndex)
                    {
                        builder.Append("\n");
                    }
                }
            }
            return builder.ToString();
        }

        private string CreateFunctionSignature(List<FunctionArgument> arguments)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(FunctionName);
            if (arguments.Count > 0)
            {
                builder.Append($"({string.Join(",", arguments)})");
            }
            return builder.ToString();
        }
    }

    public static class Functions
    {
        public static readonly List<Function> FUNCTIONS = new List<Function>();

        public static readonly Function PI = new FunctionBuilder().FunctionKey("pi").Description("Standart mathematical constant").Value(args => System.Math.PI).Build();
        public static readonly Function E = new FunctionBuilder().FunctionKey("e").Description("Standart mathematical constant").Value(args => System.Math.E).Build();
        public static readonly Function SQRT = new FunctionBuilder().FunctionKey("sqrt").Description("Square root function").Argument("n", "Input number")
            .Value(args => System.Math.Sqrt(args[0].GetValue())).Build();
        public static readonly Function SQRTX = new FunctionBuilder().FunctionKey("sqrtx").Description("Root of n function").Argument("n", "Root parameter").Argument("x", "Input value")
            .Value(args => System.Math.Pow(args[1].GetValue(), 1.0 / args[0].GetValue())).Build();
        public static readonly Function ABS = new FunctionBuilder().FunctionKey("abs").Description("Return absolute value of n").Argument("n", "Input")
            .Value(args => System.Math.Abs(args[0].GetValue())).Build();
        public static readonly Function SIN = new FunctionBuilder().FunctionKey("sin").Description("Sine function").Argument("a", "Angle in radians")
            .Value(args => System.Math.Sin(args[0].GetValue())).Build();
        public static readonly Function COS = new FunctionBuilder().FunctionKey("cos").Description("Cosine function").Argument("a", "Angle in radians")
            .Value(args => System.Math.Cos(args[0].GetValue())).Build();
        public static readonly Function TAN = new FunctionBuilder().FunctionKey("tan").Description("Tangens function").Argument("a", "Angle in radians")
            .Value(args => System.Math.Tan(args[0].GetValue())).Build();
        public static readonly Function RAD = new FunctionBuilder().FunctionKey("rad").Description("Converts degress to radians").Argument("d", "Degrees")
            .Value(args => System.Math.Sin(args[0].GetValue())).Build();
        public static readonly Function DEG = new FunctionBuilder().FunctionKey("deg").Description("Converts radians to degress").Argument("r", "Radians")
            .Value(args => System.Math.Sin(args[0].GetValue())).Build();

        internal static Function RegisterFunction(Function function)
        {
            FUNCTIONS.Add(function);
            return function;
        }

        public static Function FindFunction(string functionName)
        {
            foreach (Function function in FUNCTIONS)
            {
                if (function.FunctionName == functionName)
                {
                    return function;
                }
            }
            throw new UnknownFunctionException(functionName);
        }
    }

    public class FunctionBuilder
    {
        internal string key;
        internal string genericDescription;
        internal List<FunctionArgument> arguments;
        internal Func<IMathComponent[], double> func;

        public FunctionBuilder()
        {
            arguments = new List<FunctionArgument>();
        }

        public FunctionBuilder FunctionKey(string _key)
        {
            key = _key;
            return this;
        }

        public FunctionBuilder Description(string description)
        {
            genericDescription = description;
            return this;
        }

        public FunctionBuilder Argument(string key, string description)
        {
            if (key == null || key.Length == 0) throw new ArgumentException("Invalid argument key");
            arguments.Add(new FunctionArgument(key, description));
            return this;

        }

        public FunctionBuilder Value(Func<IMathComponent[], double> _func)
        {
            func = _func;
            return this;
        }

        public Function Build()
        {
            if (key == null || key.Length == 0) throw new ArgumentException("Invalid function key");
            if (genericDescription == null || genericDescription.Length == 0) genericDescription = "No description provided";
            if (func == null) throw new ArgumentException("Undefined number operation");
            return Functions.RegisterFunction(new Function(this));
        }
    }

    internal struct FunctionArgument
    {
        internal string ArgumentName { get; set; }
        internal string ArgumentDescription { get; set; }

        internal FunctionArgument(string argumentName, string argumentDescription)
        {
            ArgumentName = argumentName;
            ArgumentDescription = argumentDescription;
        }

        public string GetFullDescription()
        {
            if (ArgumentDescription != null)
            {
                return ArgumentName + " - " + ArgumentDescription;
            }
            return ArgumentName;
        }

        public override string ToString()
        {
            return ArgumentName;
        }
    }
}
