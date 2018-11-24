using Moq;
using Xunit;

namespace Calculator.Test
{
    public class ReversePolishNotationTransformerTest
    {
        [Fact]
        public void ReturnReversePolishNotationExpression()
        {
            var expression = "(1 + 10*2.5) / 10 - 1 = ";

            var operatorsService = new Mock<IOperatorsService>();

            operatorsService.Setup(m => m.GetPriority('+'))
                .Returns(0);
            operatorsService.Setup(m => m.GetPriority('-'))
                .Returns(0);
            operatorsService.Setup(m => m.GetPriority('*'))
                .Returns(1);
            operatorsService.Setup(m => m.GetPriority('/'))
                .Returns(1);

            operatorsService.Setup(m => m.IsSeparator('('))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator(')'))
                .Returns(false);
            operatorsService.Setup(m => m.IsSeparator(' '))
                .Returns(true);
            operatorsService.Setup(m => m.IsSeparator('='))
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

            operatorsService.Setup(m => m.IsOperator('('))
                .Returns(true);
            operatorsService.Setup(m => m.IsOperator(')'))
                .Returns(true);
            operatorsService.Setup(m => m.IsOperator(' '))
                .Returns(false);
            operatorsService.Setup(m => m.IsOperator('='))
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

            var transformer = new ReversePolishNotationTransformer(operatorsService.Object);

            var transformedExpression = transformer.TransformExpression(expression);

            Assert.Equal("1 10 2.5 * + 10 / 1 - ", transformedExpression);
        }
    }
}
