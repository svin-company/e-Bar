using Microsoft.Extensions.Configuration;

namespace eBar.Configuration;

public class ConfigReader(IConfiguration configuration): IConfigReader
{
    public T? GetConfigValue <T> (string section)
    {
        var value = configuration[section];
        return (T) Convert.ChangeType(value, typeof(T));
    }
}