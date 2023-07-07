using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services,IConfiguration configuration)
        {
            services = services ?? throw new ArgumentNullException(nameof(services));

            var rabbitMqOptions = configuration.GetSection("App:RabbitMq").Get<RabbitMqOptions>();

            services.Configure<RabbitMqOptions>(P =>
            {
                P.Host = rabbitMqOptions.Host;
                P.Port = rabbitMqOptions.Port;
                P.UserName = rabbitMqOptions.UserName;
                P.Password = rabbitMqOptions.Password;   
            });

            var connectionFactoy = new ConnectionFactory()
            {
                HostName = rabbitMqOptions.Host,
                Port = rabbitMqOptions.Port,
                UserName = rabbitMqOptions.UserName,
                Password = rabbitMqOptions.Password
            };

            services.AddSingleton<IConnectionFactory>(connectionFactoy);

            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

            services.AddSingleton<IRabbitConsumer, RabbitConsumer>();

            services.AddTransient<IRabbitEventBus, RabbitEventBus>();

            return services;
        }
    }
}
