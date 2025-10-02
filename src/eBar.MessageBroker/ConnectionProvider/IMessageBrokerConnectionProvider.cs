using eBar.MessageBroker.ConfigReader.ConfigReader;
using RabbitMQ.Client;

namespace eBar.MessageBroker.ConnectionProvider
{
    public interface IMessageBrokerConnectionProvider
    {
        public ConnectionFactory GetConnection();
    }
}
