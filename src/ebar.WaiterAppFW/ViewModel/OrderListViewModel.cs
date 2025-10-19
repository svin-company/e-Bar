using eBar.Core.Model;
using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterAppFW.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;


namespace eBar.WaiterAppFW.ViewModel
{
    public class OrderListViewModel : ViewModelBase
    {
        public ObservableCollection<Table> Tables { get; set; }
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private Table _selectedTable;
        public Table SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                LoadOrdersForSelectedTable();
            }
        }
        private ObservableCollection<Order> _ordersForSelectedTable = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrdersForSelectedTable
        {
            get => _ordersForSelectedTable;
            set
            {
                _ordersForSelectedTable = value;
                OnPropertyChanged(nameof(OrdersForSelectedTable));
            }
        }

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

        private async void LoadOrdersForSelectedTable()
        {
            if (SelectedTable == null) return;

            try
            {
                var orders = await _orderService.GetOrdersByTableIdAsync(SelectedTable.Id);
                foreach (var order in orders)
                {
                    var items = await _orderService.GetItemsByIdAsync(order.Id);
                    order.OrderItems = new ObservableCollection<OrderItem>(items);
                }
                OrdersForSelectedTable = new ObservableCollection<Order>(orders);
            }
            catch (NoRecordsException ex)
            {
                OrdersForSelectedTable.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand ChangeStatusCommand { get; }

        public OrderListViewModel(ITableService tableService, IOrderService orderService)
        {
            _tableService = tableService;
            _orderService = orderService;
            LoadTable();
            ChangeStatusCommand = new ChangeOrderStatusCommand(orderService);
        }

        private async void LoadTable()
        {
            try
            {
                var tables = await _tableService.GetAllAsync();
                Tables = new ObservableCollection<Table>(tables);
                OnPropertyChanged(nameof(Tables));
            }
            catch (NoRecordsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
