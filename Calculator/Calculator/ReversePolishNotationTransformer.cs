using System;
using System.Collections.Generic;

namespace Calculator
{
    public class ReversePolishNotationTransformer : IExpressionTransformer
    {
        public string TransformExpression(string expression)
        {
            var transformedExpression = string.Empty;
            var operStack = new Stack<char>();

            for (var i = 0; i < expression.Length; i++)
            {
                if (IsSeparator(expression[i]))
                    continue;

                if (char.IsDigit(expression[i]))
                {
                    while (!IsSeparator(expression[i]) && !IsOperator(expression[i]))
                    {
                        transformedExpression += expression[i];
                        i++;

                        if (i == expression.Length) break;
                    }

                    transformedExpression += " ";
                    i--;
                }
                else if (IsOperator(expression[i]))
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
                                if (GetPriority(expression[i]) <= GetPriority(operStack.Peek()))
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

        public static bool IsSeparator(char c)
        {
            return " =".IndexOf(c) != -1;
        }

        public static bool IsOperator(char с)
        {
            return "+-/*()".IndexOf(с) != -1;
        }

        public static int GetPriority(char c)
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
    }
}
