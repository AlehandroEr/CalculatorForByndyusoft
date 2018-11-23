using System;
using System.Collections.Generic;

namespace Calculator
{
    public class ReversePolishNotationCalculator : IExpressionCalculator
    {
        private readonly IOperatorsService _operatorsService;

        public ReversePolishNotationCalculator(IOperatorsService operatorsService)
        {
            _operatorsService = operatorsService;
        }

        public double CalculateExpression(string expression)
        {
            if (expression.Equals(""))
                return 0;

            var stack = new Stack<double>();

            for (var i = 0; i < expression.Length; i++)
            {
                if (_operatorsService.IsSeparator(expression[i]))
                    continue;

                if (char.IsDigit(expression[i]))
                {
                    var number = string.Empty;

                    while (!_operatorsService.IsSeparator(expression[i]) && !_operatorsService.IsOperator(expression[i]))
                    {
                        number += expression[i];
                        i++;
                        if (i == expression.Length) break;
                    }
                    stack.Push(double.Parse(number));
                    i--;
                }
                else if (_operatorsService.Operations.ContainsKey(expression[i]))
                    stack.Push(_operatorsService.Operations[expression[i]](stack.Pop(), stack.Pop()));
                else
                    throw new ArgumentException();
            }
            return stack.Pop();
        }
    }
}
