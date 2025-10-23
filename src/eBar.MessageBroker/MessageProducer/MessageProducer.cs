using eBar.MessageBroker.Reader;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eBar.MessageBroker.MessageProducer
{
    public class MessageProducer: IMessageProducer
    {
        private readonly IConfigReader _configReader;

        public MessageProducer(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task SendMessageAsync(string exchangeName, string queueName, string key, string message)
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
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchangeName, key, body, CancellationToken.None);
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
}
