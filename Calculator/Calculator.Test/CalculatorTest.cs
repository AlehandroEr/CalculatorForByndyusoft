using Moq;
using Xunit;

namespace Calculator.Test
{
    public class CalculatorTest
    {
        [Fact]
        public void ReturnResultOfExpression()
        {
            var expression = "2+3";
            var transformedExpression = "2 3 + ";

            var expressionTransformer = new Mock<IExpressionTransformer>();
            var expressionCalculator = new Mock<IExpressionCalculator>();

            var calculator = new Calculator(expressionTransformer.Object, expressionCalculator.Object);

            expressionTransformer.Setup(m => m.TransformExpression(expression))
                .Returns(transformedExpression);

            expressionCalculator.Setup(m => m.CalculateExpression(transformedExpression))
                .Returns(5);

            var result = calculator.CalculateResultOfExpression(expression);

            Assert.Equal(5, result);
        }
    }
}
