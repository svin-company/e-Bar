using System.Threading.Tasks;

namespace eBar.MessageBroker.MessageConsumer
{
    public interface IMessageConsumer
    {
        public Task <string> GetMessageAsync(string queueName);
    }
}
