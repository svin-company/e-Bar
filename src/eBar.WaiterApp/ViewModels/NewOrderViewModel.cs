using eBar.WaiterApp.Commands;
using eBar.WaiterApp.Models;
using eBar.WaiterApp.Storage;
using eBar.WaiterApp.Storages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace eBar.WaiterApp.ViewModels
{
    public class NewOrderViewModel: ViewModelBase
    {
        public ObservableCollection<Table> Tables { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        private Table _table;
        private Order _order;

        public event Action RequestClose;

        public Table Table
        {
            get => _table;
            set
            {
                _table = value;
                OnPropertyChanged(nameof(Table));
            }
        }
        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ConfirmCommand { get; }

        public NewOrderViewModel(Order order)
        {
            Order = order;
            Tables = TableStorage.GetAll();
            Foods = FoodStorage.GetAll();
            AddCommand = new AddToOrderCommand(Order);
            DeleteCommand = new DeleteItemCommand(Order);
            ConfirmCommand = new ConfirmCommand(Order, OnOrderConfirmed);

        }

        private void OnOrderConfirmed()
        {
            RequestClose?.Invoke();
        }
    }
}
