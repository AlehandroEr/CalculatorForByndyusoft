using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Calculator.Test
{
    public class ReversePolishNotationCalculatorTest
    {
        [Fact]
        public void ReturnResultOfReversePolishNotationExpression()
        {
            var expression = "2 3 + ";

            var operatorsService = new Mock<IOperatorsService>();

            operatorsService.Setup(m => m.IsSeparator(' '))
                .Returns(true);
            operatorsService.Setup(m => m.IsSeparator('2'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('3'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('+'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('2'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('3'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator(' '))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('+'))
                .Returns(true);
            operatorsService.Setup(m => m.Operations)
                .Returns(new Dictionary<char, Func<double, double, double>>
                {
                    {'+', (y, x) => x + y},
                    {'-', (y, x) => x - y},
                    {'*', (y, x) => x * y},
                    {'/', (y, x) => x / y}
                });

            var transformer = new ReversePolishNotationCalculator(operatorsService.Object);

            var result = transformer.CalculateExpression(expression);

            Assert.Equal(5, result);
        }
    }
}
