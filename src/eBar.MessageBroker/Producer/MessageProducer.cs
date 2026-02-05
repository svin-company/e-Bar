using eBar.Configuration;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eBar.MessageBroker.Producer
{
    public class MessageProducer: IMessageProducer
    {
        private readonly RMQConfigReader _configReader;

        public MessageProducer(RMQConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task SendMessageAsync(string exchangeName, string queueName, string key, string message)
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
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchangeName, key, body, CancellationToken.None);
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
}
