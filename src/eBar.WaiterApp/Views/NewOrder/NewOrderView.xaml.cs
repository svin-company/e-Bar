using eBar.DataStorage.Repositories.Interfaces;
using eBar.WaiterApp.ViewModel;
using System.Windows;
using eBar.Core.Model;

namespace eBar.WaiterApp.Views.NewOrder
{

    public partial class NewOrderView : Window
    {
        public Order Order { get; }
        public NewOrderView(NewOrderViewModel newOrderViewModel)
        {

            newOrderViewModel.RequestClose += () =>
            {
                this.Dispatcher.Invoke(() => this.Close());
            };
            DataContext = newOrderViewModel;
            InitializeComponent();
        }
    }
}
