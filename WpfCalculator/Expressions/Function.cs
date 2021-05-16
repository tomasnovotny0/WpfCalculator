using System;
using System.Collections.Generic;
using System.Text;
using WpfCalculator.Exceptions;

namespace WpfCalculator.Expressions
{
    /// <summary>
    /// Function represents some kind of operation which returns result based on it's inputs.
    /// For example abs(x) function return absolute value of x, or pi returns PI value
    /// </summary>
    public sealed class Function
    {
        /// <summary>
        /// Character which separates function parameters
        /// </summary>
        public static readonly char FUNCTION_PARAMETER_SEPARATOR = ',';
        /// <summary>
        /// Amount of inputs this function requires
        /// </summary>
        public int InputCount { get; private set; }
        /// <summary>
        /// Name of the function. Like 'sin'
        /// </summary>
        public string FunctionName { get; }
        /// <summary>
        /// Function signature. Composed of name and parameter names such as sqrt(n,x)
        /// </summary>
        public string FunctionSignature { get; }
        /// <summary>
        /// Used in functions window to let user know more information about this function
        /// </summary>
        public string DescriptionText { get; }
        /// <summary>
        /// Calculates result based on input parameters
        /// </summary>
        private Func<IMathComponent[], double> Func { get; }

        internal Function(FunctionBuilder builder)
        {
            InputCount = builder.arguments.Count;
            FunctionName = builder.key;
            Func = builder.func;
            DescriptionText = CreateDescriptionString(builder.genericDescription, builder.arguments);
            FunctionSignature = CreateFunctionSignature(builder.arguments);
        }

        /// <summary>
        /// Calculates value based on input parameters
        /// </summary>
        /// <param name="parameters">Input parameters. Count must match this function's expected parameter count</param>
        /// <returns>Result based on input parameters</returns>
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

    /// <summary>
    /// Static class containing common functions
    /// </summary>
    public static class Functions
    {
        public static readonly List<Function> FUNCTIONS = new List<Function>();

        public static readonly Function PI = new FunctionBuilder().FunctionKey("pi").Description("Standart mathematical constant").Value(args => Math.PI).Build();
        public static readonly Function E = new FunctionBuilder().FunctionKey("e").Description("Standart mathematical constant").Value(args => Math.E).Build();
        public static readonly Function SQRT = new FunctionBuilder().FunctionKey("sqrt").Description("Square root function").Argument("n", "Input number")
            .Value(args => Math.Sqrt(args[0].GetValue())).Build();
        public static readonly Function SQRTX = new FunctionBuilder().FunctionKey("sqrtx").Description("Root of n function").Argument("n", "Root parameter").Argument("x", "Input value")
            .Value(args => Math.Pow(args[1].GetValue(), 1.0 / args[0].GetValue())).Build();
        public static readonly Function ABS = new FunctionBuilder().FunctionKey("abs").Description("Return absolute value of n").Argument("n", "Input")
            .Value(args => Math.Abs(args[0].GetValue())).Build();
        public static readonly Function SIN = new FunctionBuilder().FunctionKey("sin").Description("Sine function").Argument("a", "Angle in radians")
            .Value(args => Math.Sin(args[0].GetValue())).Build();
        public static readonly Function COS = new FunctionBuilder().FunctionKey("cos").Description("Cosine function").Argument("a", "Angle in radians")
            .Value(args => Math.Cos(args[0].GetValue())).Build();
        public static readonly Function TAN = new FunctionBuilder().FunctionKey("tan").Description("Tangens function").Argument("a", "Angle in radians")
            .Value(args => Math.Tan(args[0].GetValue())).Build();
        public static readonly Function RAD = new FunctionBuilder().FunctionKey("rad").Description("Converts degress to radians").Argument("d", "Degrees")
            .Value(args => Math.Sin(args[0].GetValue())).Build();
        public static readonly Function DEG = new FunctionBuilder().FunctionKey("deg").Description("Converts radians to degress").Argument("r", "Radians")
            .Value(args => Math.Sin(args[0].GetValue())).Build();

        internal static Function RegisterFunction(Function function)
        {
            FUNCTIONS.Add(function);
            return function;
        }

        /// <summary>
        /// Finds function by it's key
        /// </summary>
        /// <param name="functionName">Name of the function you're looking for</param>
        /// <returns>Function which has <paramref name="functionName"/> or throws exception when no such function exists</returns>
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

    /// <summary>
    /// Builder structure to create function instances
    /// </summary>
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

        /// <summary>
        /// Sets function key
        /// </summary>
        /// <param name="_key">Key for this function</param>
        public FunctionBuilder FunctionKey(string _key)
        {
            key = _key;
            return this;
        }

        /// <summary>
        /// Sets function description
        /// </summary>
        /// <param name="description">Description for this function</param>
        public FunctionBuilder Description(string description)
        {
            genericDescription = description;
            return this;
        }

        /// <summary>
        /// Adds function argument
        /// </summary>
        /// <param name="key">Argument key - will be seen in function signature</param>
        /// <param name="description">Argument description</param>
        public FunctionBuilder Argument(string key, string description)
        {
            if (key == null || key.Length == 0) throw new ArgumentException("Invalid argument key");
            arguments.Add(new FunctionArgument(key, description));
            return this;

        }

        /// <summary>
        /// Defines how this function calculates it's result based on arguments
        /// </summary>
        /// <param name="_func">Operation</param>
        public FunctionBuilder Value(Func<IMathComponent[], double> _func)
        {
            func = _func;
            return this;
        }

        /// <summary>
        /// Validates values in this builder and constructs new function object
        /// </summary>
        /// <returns>New function instance</returns>
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

        internal string GetFullDescription()
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
