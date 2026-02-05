using eBar.Configuration;

namespace eBar.MessageBroker
{
    public class RMQConfigReader
    {
        public string Connection { get; set; }
        public RMQConfigReader(IConfigReader configReader)
        {
            Connection = configReader.GetConfigValue<string>("MessageBroker:MessageBrokerConnection");
        }
    }
}
