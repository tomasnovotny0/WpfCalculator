﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Math
{
    public class OperatorComponent : IMathComponent
    {
        public Operator Operator { get; }

        public OperatorComponent(Operator @operator)
        {
            Operator = @operator;
        }

        public bool IsValueType()
        {
            return false;
        }

        public double GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
