using System;
using System.Collections.Generic;

namespace Calculator
{
    public class OperatorsService : IOperatorsService
    {
        public Dictionary<char, Func<double, double, double>> Operations { get; }

        public OperatorsService()
        {
            Operations = new Dictionary<char, Func<double, double, double>>
            {
                {'+', (y, x) => x + y},
                {'-', (y, x) => x - y},
                {'*', (y, x) => x * y},
                {'/', (y, x) => x / y}
            };
        }

        public int GetPriority(char c)
        {
            switch (c)
            {
                case '+': return 0;
                case '-': return 0;
                case '*': return 1;
                case '/': return 1;
                default: throw new ArgumentException();
            }
        }

        public bool IsOperator(char c)
        {
            return "+-/*()".IndexOf(c) != -1;
        }

        public bool IsSeparator(char c)
        {
            return " =".IndexOf(c) != -1;
        }
    }
}
