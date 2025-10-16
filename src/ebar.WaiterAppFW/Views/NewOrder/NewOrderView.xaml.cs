using eBar.WaiterAppFW.Model;
using eBar.WaiterAppFW.ViewModel;
using System.Windows;


namespace ebar.WaiterAppFW.Views.NewOrder
{
    public partial class NewOrderView : Window
    {
        public NewOrderView()
        {
            var order = new Order();
            var newOrderViewModel = new NewOrderViewModel(order);
            newOrderViewModel.RequestClose += () => this.Close();
            DataContext = newOrderViewModel;
            InitializeComponent();

        }
    }
}
