using eBar.WaiterAppFW.Commands;
using eBar.Core.Model;

namespace eBar.WaiterAppFW.ViewModel
{
    public class NewOrderViewModel: ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly IFoodService _foodService;
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

        public NewOrderViewModel(Order order, ITableService tableService, IFoodService foodService)
        {
            _tableService = tableService;
            _foodS
            Order = order;
            Tables = _tableService.GetAll();
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
