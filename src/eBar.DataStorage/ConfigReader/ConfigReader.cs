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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                ?? config.GetConnectionString(connectionKey);
            return connectionString;
        }
    }
}
