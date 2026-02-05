using eBar.DataStorage.Services;
using eBar.DataStorage.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eBar.DataStorage
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IWaiterService, WaiterService>();
            return services;
        }
    }
}
