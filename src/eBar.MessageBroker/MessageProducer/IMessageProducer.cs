using System.Threading.Tasks;

namespace eBar.MessageBroker.MessageProducer
{
    public interface IMessageProducer
    {
        public Task SendMessageAsync(string exchangeName, string queueName, string key, string message);
    }
}
