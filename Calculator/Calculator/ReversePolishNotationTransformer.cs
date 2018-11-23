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
                    while (!_operatorsService.IsSeparator(expression[i]) && !_operatorsService.IsOperator(expression[i]))
                    {
                        transformedExpression += expression[i];
                        i++;

                        if (i == expression.Length) break;
                    }

                    transformedExpression += " ";
                    i--;
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

    }
}
