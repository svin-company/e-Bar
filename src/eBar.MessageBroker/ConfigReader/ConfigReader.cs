using Microsoft.Extensions.Configuration;

namespace eBar.MessageBroker.ConfigReader.ConfigReader
{
    public class ConfigReader : IConfigReader
    {
        public IConfigurationSection GetConnectionSettings()
        {
            string connectionKey = "MessageBrokerConnection";
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return config.GetSection(connectionKey);
        }
    }
}
