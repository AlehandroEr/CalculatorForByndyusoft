using Moq;
using Xunit;

namespace Calculator.Test
{
    public class ReversePolishNotationTransformerTest
    {
        [Fact]
        public void TransformExpression()
        {
            var expression = "2 + 3";

            var operatorsService = new Mock<IOperatorsService>();

            operatorsService.Setup(m => m.GetPriority('+'))
                .Returns(0);
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

            var transformer = new ReversePolishNotationTransformer(operatorsService.Object);

            var transformedExpression = transformer.TransformExpression(expression);

            Assert.Equal("2 3 + ", transformedExpression);
        }
    }
}
