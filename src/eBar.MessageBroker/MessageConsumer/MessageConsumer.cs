using eBar.MessageBroker.ConfigReader.ConfigReader;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace eBar.MessageBroker.MessageConsumer
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IConfigReader _configReader;

        public MessageConsumer(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<string> GetMessageAsync(string queueName)
        {
            var connectionFactory = new ConnectionFactory();
            var section = _configReader.GetConnectionSettings();
            section.Bind(connectionFactory);

            await using var connection = await connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var tcs = new TaskCompletionSource<string>();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                tcs.TrySetResult(message);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: queueName,
                autoAck: true,
                consumer: consumer);

            return await tcs.Task;

        }
    }
}
