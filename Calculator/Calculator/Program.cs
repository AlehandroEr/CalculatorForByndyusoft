using System;
using System.Reflection;
using Ninject;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var calculator = kernel.Get<Calculator>();

            Console.Write("Введите выражение: ");

            var result = calculator.CalculateResultOfExpression(Console.ReadLine());

            Console.WriteLine("Результат: " + result);
            Console.ReadKey();
        }
    }
}
