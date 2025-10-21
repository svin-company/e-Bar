using eBar.WaiterApp.Storages;
using eBar.WaiterApp.ViewModel;
using System.Windows;

namespace eBar.WaiterApp.Views
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
