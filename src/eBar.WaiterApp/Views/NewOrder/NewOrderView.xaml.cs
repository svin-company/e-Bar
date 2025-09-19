using eBar.WaiterApp.Models;
using eBar.WaiterApp.ViewModels;
using System.Windows;


namespace eBar.WaiterApp.Views
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
