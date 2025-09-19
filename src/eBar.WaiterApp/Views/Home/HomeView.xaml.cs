using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eBar.WaiterApp.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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