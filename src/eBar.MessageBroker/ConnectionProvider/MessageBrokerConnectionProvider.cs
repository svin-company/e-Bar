using eBar.MessageBroker.ConfigReader.ConfigReader;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace eBar.MessageBroker.ConnectionProvider
{
    public class MessageBrokerConnectionProvider: IMessageBrokerConnectionProvider
    {
        private readonly IConfigReader _configReader;

        public MessageBrokerConnectionProvider(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public ConnectionFactory GetConnection()
        {

            var connectionSection = _configReader.GetConnectionSettings();
            ConnectionFactory connection = null;
            if (connectionSection.Exists())
            {
                connection = new ConnectionFactory
                {
                    HostName = connectionSection["HostName"],
                    Port = int.Parse(connectionSection["Port"]),
                    UserName = connectionSection["UserName"],
                    Password = connectionSection["Password"],
                    VirtualHost = "/"
                };
            }
            return connection;
            
        }
    }
}
