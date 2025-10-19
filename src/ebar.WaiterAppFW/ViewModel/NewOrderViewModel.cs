using eBar.WaiterAppFW.Commands;
using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using eBar.DataStorage.Exceptions;
namespace eBar.WaiterAppFW.ViewModel
{
    public class NewOrderViewModel: ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;
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

        public NewOrderViewModel(Order order, ITableService tableService, IFoodService foodService, IOrderService orderService)
        {
            _foodService = foodService;
            _tableService = tableService;
            _orderService = orderService;
            Order = order;
            LoadTables();
            AddCommand = new AddToOrderCommand(Order, orderService);
            DeleteCommand = new DeleteItemCommand(Order, orderService);
            ConfirmCommand = new ConfirmCommand(Order, orderService, OnOrderConfirmed);

        }

        private async void LoadTables()
        {
            try
            {
                var tables = await _tableService.GetAllAsync();
                Tables = new ObservableCollection<Table>(tables);
                OnPropertyChanged(nameof(Tables));

                var foods = await _foodService.GetAllAsync();
                Foods = new ObservableCollection<Food>(foods);
                OnPropertyChanged(nameof(Foods));
            }
            catch (NoRecordsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnOrderConfirmed()
        {
            RequestClose?.Invoke();
        }
    }
}
