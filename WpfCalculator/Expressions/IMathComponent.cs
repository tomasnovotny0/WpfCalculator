﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Expressions
{
    public interface IMathComponent
    {
        bool Negative { get; set; }

        double GetValue();
    }
}