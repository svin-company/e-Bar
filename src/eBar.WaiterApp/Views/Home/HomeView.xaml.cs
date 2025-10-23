using eBar.WaiterApp.Views.NewOrder;
using eBar.WaiterApp.Views.OrdersList;
using System.Windows;

namespace eBar.WaiterApp.Views.Home
{
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
