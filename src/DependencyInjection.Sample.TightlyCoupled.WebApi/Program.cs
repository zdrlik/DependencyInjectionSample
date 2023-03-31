
using DependencyInjection.Sample.TightlyCoupled.Core;

namespace DependencyInjection.Sample.TightlyCoupled.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddLogging(builder => builder
                .AddFilter("*", LogLevel.Debug)
                .AddConsole());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            RuntimeConfiguration.Initialize(serviceProvider.GetRequiredService<IConfiguration>());
            LogManager.LoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        }


    }
}