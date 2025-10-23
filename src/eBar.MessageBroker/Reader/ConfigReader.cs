using System;
using Microsoft.Extensions.Configuration;

namespace eBar.MessageBroker.Reader
{
    public class ConfigReader : IConfigReader
    {
        public IConfigurationSection GetConnectionSettings()
        {
            string connectionKey = "MessageBrokerConnection";
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var section =  config.GetSection(connectionKey);
            return section;
        }
    }
}
