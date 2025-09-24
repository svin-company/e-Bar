using Microsoft.Extensions.Configuration;

namespace eBar.DataStorage.ConfigReader
{
    public class ConfigReader: IConfigReader
    {
        public string GetConnectionString()
        {
            string connectionKey = "DbConnection";
            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            return config.GetConnectionString(connectionKey);
        }
    }
}
