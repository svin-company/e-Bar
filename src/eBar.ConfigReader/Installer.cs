using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eBar.Configuration;

public static class Installer
{
    public static  IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(config);
        services.AddSingleton<IConfigReader, ConfigReader>();

        return services;
    }
}