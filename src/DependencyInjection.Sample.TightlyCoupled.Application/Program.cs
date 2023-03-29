using DependencyInjection.Sample.TightlyCoupled.Core;

namespace DependencyInjection.Sample.TightlyCoupled.Application
{
    internal class Program
    {
        static async Task Main()
        {
            var productsUi = new ListProductsUi();
            await productsUi.ShowProducts();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}