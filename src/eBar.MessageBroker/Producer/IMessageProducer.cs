using System.Threading.Tasks;

namespace eBar.MessageBroker.Producer
{
    public interface IMessageProducer
    {
        public Task SendMessageAsync(string exchangeName, string queueName, string key, string message);
    }
}
