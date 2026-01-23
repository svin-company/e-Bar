using eBar.WaiterApp.Views.NewOrder;
using eBar.WaiterApp.Views.OrdersList;
using System.Windows;

namespace eBar.WaiterApp.Views.Home
{
    public partial class HomeView : Window
    {
        private readonly NewOrderView _newOrderView;
        private readonly OrdersListView _ordersListView;
        public HomeView(NewOrderView newOrderView, OrdersListView ordersListView)
        {
            _newOrderView = newOrderView;
            _ordersListView = ordersListView;
            InitializeComponent();
        }

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        { 
            _newOrderView.ShowDialog();
        }

        private void OrdersListButton_Click(object sender, RoutedEventArgs e)
        {
            _ordersListView.ShowDialog();
        }
    }
}
