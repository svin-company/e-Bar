using eBar.WaiterApp.ViewModel;
using eBar.WaiterApp.Views.NewOrder;
using eBar.WaiterApp.Views.OrdersList;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace eBar.WaiterApp.Views.Home
{
    public partial class HomeView : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public HomeView(HomeViewModel homeViewModel)
        {
            DataContext = homeViewModel;
            InitializeComponent();
        }

    }
}
