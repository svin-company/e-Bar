using eBar.WaiterApp.Commands;
using eBar.WaiterApp.Models;
using eBar.WaiterApp.Storages;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace eBar.WaiterApp.ViewModels
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
