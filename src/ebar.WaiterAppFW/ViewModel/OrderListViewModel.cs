using eBar.WaiterAppFW.Commands;
using eBar.WaiterAppFW.Model;
using eBar.WaiterAppFW.Storages;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace eBar.WaiterAppFW.ViewModel
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
