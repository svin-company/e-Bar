using Microsoft.Extensions.Configuration;

namespace eBar.MessageBroker.Reader
{
    public interface IConfigReader
    {
        public IConfigurationSection GetConnectionSettings();
    }
}
