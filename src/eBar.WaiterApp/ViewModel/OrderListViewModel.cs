using eBar.Core.Model;
using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace eBar.WaiterApp.ViewModel
{
    public class OrderListViewModel : ViewModelBase
    {
        public ObservableCollection<TableViewModel> Tables { get; set; }
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IWaiterService _waiterService;
        private TableViewModel _selectedTable;
        public TableViewModel SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                _ = LoadOrdersForSelectedTable();
            }
        }

        public ObservableCollection<OrderViewModel> OrdersForSelectedTable { get; set; }

        private OrderViewModel _selectedOrder; 
        public OrderViewModel SelectedOrder 
        { 
            get => _selectedOrder; 
            set 
            { 
                _selectedOrder = value; 
                OnPropertyChanged(nameof(SelectedOrder));
            } 
        }

        private async Task LoadOrdersForSelectedTable()
        {
            if (SelectedTable == null) return;

            try
            {
                OrdersForSelectedTable.Clear();
                var orders = await _orderService.GetOrdersByTableIdAsync(SelectedTable.Id);
                foreach (var order in orders)
                {
                    var items = await _orderService.GetItemsByIdAsync(order.Id);

                    order.OrderItems = items;
                    order.WaiterName = await _waiterService.GetByIdAsync(order.WaiterId);
                }
                foreach (var item in orders)
                {
                    var orderVM = new OrderViewModel(item);
                    OrdersForSelectedTable.Add(orderVM);
                }
                
            }
            catch (NoRecordsException ex)
            {
                OrdersForSelectedTable.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand ChangeStatusCommand { get; }

        public OrderListViewModel(ITableService tableService, IOrderService orderService, IWaiterService waiterService)
        {
            _tableService = tableService;
            _orderService = orderService;
            _waiterService = waiterService;
            OrdersForSelectedTable = new ObservableCollection<OrderViewModel>();
            LoadTable();
            ChangeStatusCommand = new ChangeOrderStatusCommand(orderService);
        }

        private async void LoadTable()
        {
            try
            {
                Tables = new ObservableCollection<TableViewModel>();
                var tables = await _tableService.GetAllAsync();
                foreach (var item in tables)
                {
                    var table = new TableViewModel(item);
                    Tables.Add(table);
                }
                OnPropertyChanged(nameof(Tables));
            }
            catch (NoRecordsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
