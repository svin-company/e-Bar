using eBar.MessageBroker.ConnectionProvider;

namespace eBar.MessageBroker.MessageProducer
{
    public class MessageProducer: IMessageProducer
    {
        private readonly IMessageBrokerConnectionProvider _mBConnectionProvider;

        public MessageProducer(IMessageBrokerConnectionProvider mBConnectionProvider)
        {
            _mBConnectionProvider = mBConnectionProvider;
        }

        public async Task SendMessageAsync(string message)
        {
            using(_mBConnectionProvider = new)
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
}
