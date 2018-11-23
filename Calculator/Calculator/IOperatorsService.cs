using System;
using System.Collections.Generic;

namespace Calculator
{
    public interface IOperatorsService
    {
        Dictionary<char, Func<double, double, double>> Operations { get; }

        int GetPriority(char c);
        bool IsOperator(char c);
        bool IsSeparator(char c);
    }
}
