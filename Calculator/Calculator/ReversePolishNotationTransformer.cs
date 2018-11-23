using System;
using System.Collections.Generic;

namespace Calculator
{
    public class ReversePolishNotationTransformer : IExpressionTransformer
    {
        private readonly IOperatorsService _operatorsService;

        public ReversePolishNotationTransformer(IOperatorsService operatorsService)
        {
            _operatorsService = operatorsService;
        }

        public string TransformExpression(string expression)
        {
            var transformedExpression = string.Empty;
            var operStack = new Stack<char>();

            for (var i = 0; i < expression.Length; i++)
            {
                if (_operatorsService.IsSeparator(expression[i]))
                    continue;

                if (char.IsDigit(expression[i]))
                {
                    var number = TakeNumber(expression, i);
                    transformedExpression += number + " ";
                    i += number.Length - 1;
                }
                else if (_operatorsService.IsOperator(expression[i]))
                {
                    if (expression[i] == '(')
                        operStack.Push(expression[i]);
                    else if (expression[i] == ')')
                    {
                        var oper = operStack.Pop();

                        while (oper != '(')
                        {
                            transformedExpression += oper.ToString() + ' ';
                            oper = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (operStack.Peek() != '(')
                                if (_operatorsService.GetPriority(expression[i]) <= _operatorsService.GetPriority(operStack.Peek()))
                                    transformedExpression += operStack.Pop() + " ";

                        operStack.Push(expression[i]);

                    }
                }
                else
                    throw new ArgumentException();

            }

            while (operStack.Count > 0)
                transformedExpression += operStack.Pop() + " ";

            return transformedExpression;
        }

        private string TakeNumber(string expression, int position)
        {
            var number = string.Empty;

            while (!_operatorsService.IsSeparator(expression[position]) && !_operatorsService.IsOperator(expression[position]))
            {
                number += expression[position];
                position++;

                if (position == expression.Length) break;
            }

            return number;
        }
    }
}
