using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using ReciverConsoleAsync.Consumers;

namespace ReciverConsoleAsync
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        
                        x.AddConsumer<ReciverConsoleAsyncConsumer>(); // Register the consumer

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context); // Configure endpoints for consumers
                            // cfg.ReceiveEndpoint("test_queue", e =>
                            // {
                            //     e.ConfigureConsumer<ReciverConsoleAsyncConsumer>(context);
                            // });
                        });
                    });

                    // services.AddHostedService<Worker>(); // Add any hosted services if needed
                });
    }
}
