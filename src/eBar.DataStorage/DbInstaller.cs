using eBar.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eBar.DataStorage
{
    public static class DbInstaller
    {
        // Пока не уверена, что это так будет выглядеть
        public static IServiceCollection AddDataBase(this IServiceCollection services)
        {
            services.AddConfiguration();
            services.AddTransient<DbConfigReader>();
            return services;
        }
    }
}
