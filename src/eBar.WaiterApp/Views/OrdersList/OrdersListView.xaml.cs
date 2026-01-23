using eBar.WaiterApp.ViewModel;
using System.Windows;


namespace eBar.WaiterApp.Views.OrdersList
{
    public partial class OrdersListView : Window
    {
        public OrdersListView(OrderListViewModel orderListViewModel)
        {
            DataContext = orderListViewModel;
            InitializeComponent();
        }
    }
}
