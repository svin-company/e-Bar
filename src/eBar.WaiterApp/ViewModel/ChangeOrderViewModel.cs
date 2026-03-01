using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.Commands;
using eBar.WaiterApp.Service.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace eBar.WaiterApp.ViewModel
{
    public class ChangeOrderViewModel: ViewModelBase
    {
        private OrderViewModel _order;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;
        private readonly IOrderAppService _orderAppService;
        public ObservableCollection<Food> Foods { get; set; }
        public event Action RequestClose;
        public OrderViewModel Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        public ICommand ConfirmCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ChangeOrderViewModel(OrderViewModel order, IOrderService orderService, 
            IFoodService foodService, IOrderAppService orderAppservice)
        {
            Order = order;
            _orderService = orderService;
            _foodService = foodService;
            _orderAppService = orderAppservice;
            GetFood();

            AddCommand = new AddToOrderCommand(Order, _orderAppService);
            DeleteCommand = new DeleteItemCommand(Order, _orderAppService);
            ConfirmCommand = new ConfirmOrderUpdateCommand(Order, _orderService, OnOrderConfirmed);
        }

        private async void GetFood()
        {
            var foods = await _foodService.GetAllAsync();
            Foods = new ObservableCollection<Food>(foods);
            OnPropertyChanged(nameof(Foods));
        }

        private void OnOrderConfirmed()
        {
            RequestClose?.Invoke();
        }
    }
}
