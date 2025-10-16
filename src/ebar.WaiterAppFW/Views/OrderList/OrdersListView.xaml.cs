using eBar.WaiterAppFW.ViewModel;
using System.Windows;


namespace ebar.WaiterAppFW.Views.OrderList
{
    public partial class OrdersListView : Window
    {
        public OrdersListView()
        {
            DataContext = new OrderListViewModel();
            InitializeComponent();
        }
    }
}
