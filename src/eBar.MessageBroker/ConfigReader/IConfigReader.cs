using Microsoft.Extensions.Configuration;

namespace eBar.MessageBroker.ConfigReader.ConfigReader
{
    public interface IConfigReader
    {
        public IConfigurationSection GetConnectionSettings();
    }
}
