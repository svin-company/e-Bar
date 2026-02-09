using System.Threading.Tasks;

namespace eBar.MessageBroker.Consumer
{
    public interface IMessageConsumer
    {
        public Task <string> GetMessageAsync(string queueName);
    }
}
