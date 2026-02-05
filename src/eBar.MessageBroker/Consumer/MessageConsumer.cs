using eBar.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace eBar.MessageBroker.Consumer
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly RMQConfigReader _configReader;

        public MessageConsumer(RMQConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<string> GetMessageAsync(string queueName)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_configReader.Connection)
            };

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
