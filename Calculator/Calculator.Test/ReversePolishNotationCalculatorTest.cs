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
            var expression = "1 10 2.5 * + 10 / 1 - ";

            var operatorsService = new Mock<IOperatorsService>();
            
            operatorsService.Setup(m => m.IsSeparator(' '))
                .Returns(true);
            operatorsService.Setup(m => m.IsSeparator('.'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('0'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('1'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('2'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('5'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('+'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('-'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('*'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator('/'))
                .Returns(false);
            
            operatorsService.Setup(m => m.IsOperator(' '))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('.'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('0'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('1'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('2'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('5'))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('+'))
                .Returns(true);
            operatorsService.Setup(m => m.IsOperator('-'))
                .Returns(true);
            operatorsService.Setup(m => m.IsOperator('*'))
                .Returns(true);
            operatorsService.Setup(m => m.IsOperator('/'))
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

            Assert.Equal(1.6, result);
        }
    }
}
