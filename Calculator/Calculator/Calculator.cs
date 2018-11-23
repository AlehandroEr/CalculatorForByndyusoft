namespace Calculator
{
    public class Calculator
    {
        private readonly IExpressionTransformer _expressionTransformer;
        private readonly IExpressionCalculator _expressionCalculator;

        public Calculator(IExpressionTransformer expressionBuilder, IExpressionCalculator expressionCalculator)
        {
            _expressionTransformer = expressionBuilder;
            _expressionCalculator = expressionCalculator;
        }

        public double CalculateResultOfExpression(string expression)
        {
            var transformedExpression = _expressionTransformer.TransformExpression(expression);

            return _expressionCalculator.CalculateExpression(transformedExpression);
        }
    }
}
