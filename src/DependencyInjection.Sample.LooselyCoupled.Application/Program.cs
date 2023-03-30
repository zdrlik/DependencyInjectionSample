namespace DependencyInjection.Sample.LooselyCoupled.Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // var services = (await new ManualCompositionRoot().ConfigureServices());
            var services = (await new ServiceCollectionCompositionRoot().ConfigureServices());
            var productsUi = services.ProductsUi;
            await productsUi.ShowProducts();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}