using eBar.Configuration;
using eBar.DataStorage;
using eBar.DataStorage.Repositories;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.Service;
using eBar.WaiterApp.ViewModel;
using eBar.WaiterApp.Views.Home;
using eBar.WaiterApp.Views.NewOrder;
using eBar.WaiterApp.Views.OrdersList;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace eBar.WaiterApp
{

    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) => 
                {
                    services.AddTransient<HomeView>();
                    services.AddTransient<NewOrderView>();
                    services.AddTransient<OrdersListView>();
                    services.AddTransient<NewOrderViewModel>();
                    services.AddTransient<OrderListViewModel>();
                    services.AddTransient<HomeViewModel>();
                    services.AddTransient<OrderViewModel>();
                    services.AddConfiguration();
                    services.AddTransient<DbConfigReader>();
                    services.AddRepositories();
                    services.AddServices();
                    services.AddTransient<IOrderAppService, OrderAppService>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startWindow = AppHost.Services.GetRequiredService<HomeView>();
            startWindow.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }


}
