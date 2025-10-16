using ebar.WaiterAppFW.Views.NewOrder;
using ebar.WaiterAppFW.Views.OrderList;
using System.Windows;

namespace ebar.WaiterAppFW.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var newOrderView = new NewOrderView();
            newOrderView.ShowDialog();
        }

        private void OrdersListButton_Click(object sender, RoutedEventArgs e)
        {
            var ordersListView = new OrdersListView();
            ordersListView.ShowDialog();
        }
    }
}
