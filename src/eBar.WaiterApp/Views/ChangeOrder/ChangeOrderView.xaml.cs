using eBar.WaiterApp.ViewModel;
using System.Windows;

namespace eBar.WaiterApp.Views.ChangeOrder
{
    public partial class ChangeOrderView : Window
    {
        public ChangeOrderView(ChangeOrderViewModel changeOrderVM)
        {
            changeOrderVM.RequestClose += () =>
            {
                this.Dispatcher.Invoke(() => this.Close());
            };
            DataContext = changeOrderVM;
            InitializeComponent();
        }
    }
}
