using eBar.DataStorage.Repositories;
using eBar.DataStorage.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace eBar.DataStorage
{
    public static class RepositoriesInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IWaiterRepository, WaiterRepository>();

            return services;
        }
    }
}
