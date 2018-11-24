using System;
using System.Collections.Generic;
using System.Text;

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
                    var number = TakeNumber(expression, i);
                    stack.Push(double.Parse(number));
                    i += number.Length - 1;
                }
                else if (_operatorsService.Operations.ContainsKey(expression[i]))
                    stack.Push(_operatorsService.Operations[expression[i]](stack.Pop(), stack.Pop()));
                else
                    throw new ArgumentException();
            }
            return stack.Pop();
        }

        private string TakeNumber(string expression, int position)
        {
            var number = new StringBuilder();

            while (!_operatorsService.IsSeparator(expression[position]) && !_operatorsService.IsOperator(expression[position]))
            {
                number.Append(expression[position]);
                position++;

                if (position == expression.Length) break;
            }

            return number.ToString();
        }
    }
}
