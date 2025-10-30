using eBar.WaiterApp.Commands;
using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using eBar.DataStorage.Exceptions;
using eBar.WaiterApp.Service;
namespace eBar.WaiterApp.ViewModel
{
    public class NewOrderViewModel: ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;
        private readonly IOrderAppService _orderAppService;
        private readonly IWaiterService _waiterService;
        public ObservableCollection<TableViewModel> Tables { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<Waiter> Waiters { get; set; }
        private TableViewModel _table;
        private OrderViewModel _order;

        public event Action RequestClose;

        public TableViewModel Table
        {
            get => _table;
            set
            {
                _table = value;
                OnPropertyChanged(nameof(TableViewModel));
            }
        }
        public OrderViewModel Order
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

        public NewOrderViewModel(OrderViewModel order, ITableService tableService, 
            IFoodService foodService, IOrderService orderService, IWaiterService waiterService, IOrderAppService orderAppService)
        {
            _foodService = foodService;
            _tableService = tableService;
            _orderService = orderService;
            _waiterService = waiterService;
            _orderAppService = orderAppService;
            Order = order;
            LoadTables();
            AddCommand = new AddToOrderCommand(Order, _orderAppService);
            DeleteCommand = new DeleteItemCommand(Order, _orderAppService);
            ConfirmCommand = new ConfirmCommand(Order, _orderService, OnOrderConfirmed);
        }

        private async void LoadTables()
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

                var foods = await _foodService.GetAllAsync();
                Foods = new ObservableCollection<Food>(foods);
                OnPropertyChanged(nameof(Foods));

                var waiters = await _waiterService.GetAllAsync();
                Waiters = new ObservableCollection<Waiter>(waiters);
                OnPropertyChanged(nameof(Waiters));
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
