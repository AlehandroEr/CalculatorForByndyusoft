using Xunit;

namespace Calculator.Test
{
    public class ReversePolishNotationTransformerTest
    {
        [Fact]
        public void TransformExpression()
        {
            var expression = "2 + 3";

            var transformer = new ReversePolishNotationTransformer();

            var transformedExpression = transformer.TransformExpression(expression);

            Assert.Equal("2 3 + ", transformedExpression);
        }
    }
}
