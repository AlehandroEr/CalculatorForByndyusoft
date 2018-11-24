using System;
using System.Collections.Generic;
using System.Text;

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
            var transformedExpression = new StringBuilder();
            var operStack = new Stack<char>();

            for (var i = 0; i < expression.Length; i++)
            {
                if (_operatorsService.IsSeparator(expression[i]))
                    continue;

                if (char.IsDigit(expression[i]))
                {
                    var number = TakeNumber(expression, i);
                    transformedExpression.Append(number + " ");
                    i += number.Length - 1;
                }
                else if (_operatorsService.IsOperator(expression[i]))
                {
                    if (expression[i] == '(')
                        operStack.Push(expression[i]);
                    else if (expression[i] == ')')
                    {
                        transformedExpression.Append(TakeAllArithmeticOperatorsUntilOpeningBracket(operStack));
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (operStack.Peek() != '(')
                                if (_operatorsService.GetPriority(expression[i]) <= _operatorsService.GetPriority(operStack.Peek()))
                                    transformedExpression.Append(operStack.Pop() + " ");

                        operStack.Push(expression[i]);

                    }
                }
                else
                    throw new ArgumentException();

            }

            while (operStack.Count > 0)
                transformedExpression.Append(operStack.Pop() + " ");

            return transformedExpression.ToString();
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

        private string TakeAllArithmeticOperatorsUntilOpeningBracket(Stack<char> operStack)
        {
            var operations = new StringBuilder();
            var oper = operStack.Pop();

            while (oper != '(')
            {
                operations.Append(oper.ToString() + ' ');
                oper = operStack.Pop();
            }

            return operations.ToString();
        }
    }
}
