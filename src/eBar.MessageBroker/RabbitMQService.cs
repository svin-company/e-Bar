using eBar.MessageBroker.Consumer;
using eBar.MessageBroker.Producer;
using Microsoft.Extensions.DependencyInjection;
namespace eBar.MessageBroker
{
    public static class RabbitMQService
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();

            return services;
        }
    }
}
