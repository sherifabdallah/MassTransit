using Microsoft.Extensions.Hosting;
using MassTransit;
using ReciverConsoleSync.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace ReciverConsoleSync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<ReciverConsoleSyncConsumer>(); // Register the consumer

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context); // Configure endpoints for consumers
                            cfg.ReceiveEndpoint("test_queue", e =>
                            {
                                e.ConfigureConsumer<ReciverConsoleSyncConsumer>(context);
                            });
                        });
                    });
                });
    }
}
