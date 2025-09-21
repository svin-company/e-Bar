using eBar.WaiterApp.Commands;
using eBar.WaiterApp.Model;
using eBar.WaiterApp.Storage;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace eBar.WaiterApp.ViewModel
{
    public class OrderListViewModel : ViewModelBase
    {
        public ObservableCollection<Table> Tables { get; set; }
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public ICommand ChangeStatusCommand { get; }

        public OrderListViewModel()
        {
            Tables = TableStorage.GetAll();
            ChangeStatusCommand = new ChangeOrderStatusCommand();
        }
    }
}
