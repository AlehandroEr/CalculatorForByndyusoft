using Ninject.Modules;

namespace Calculator
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<Calculator>().ToSelf();
            Bind<IOperatorsService>().To<OperatorsService>();
            Bind<IExpressionTransformer>().To<ReversePolishNotationTransformer>();
            Bind<IExpressionCalculator>().To<ReversePolishNotationCalculator>();
        }
    }
}
