using MassTransit;
using SenderApi.Controllers;
namespace SenderApi;

public static class ConfigurationService
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
          {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
               

            });

        });
        //   services.AddHostedService<SendMessageController>();

        return services;
    }
}