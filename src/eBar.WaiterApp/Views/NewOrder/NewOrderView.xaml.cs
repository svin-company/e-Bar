using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Repositories;
using eBar.DataStorage.Services.Interfaces;
using eBar.DataStorage.Services;
using eBar.WaiterApp.ViewModel;
using System.Windows;
using eBar.Core.Model;
using eBar.WaiterApp.Service;

namespace eBar.WaiterApp.Views.NewOrder
{

    public partial class NewOrderView : Window
    {
        public NewOrderView()
        {
            var reader = new ConfigReader();

            ITableRepository tableRepository = new TableRepository(reader);
            ITableService tableService = new TableService(tableRepository);

            IFoodRepository foodRepository = new FoodRepository(reader);

            IOrderItemRepository orderItemRepository = new OrderItemRepository(reader);
            IOrderRepository orderRepository = new OrderRepository(reader);
            IOrderService orderService = new OrderService(orderRepository, orderItemRepository);
            IFoodService foodService = new FoodService(foodRepository);

            IWaiterRepository waiterRepository = new WaiterRepository(reader);
            IWaiterService waiterService = new WaiterService(waiterRepository);

            IOrderAppService orderAppService = new OrderAppService();

            var order = new Order
            {
                IsOrderOpen = true,
                OrderTime = DateTime.Now
            };

            var newOrderViewModel = new NewOrderViewModel (new OrderViewModel(order), tableService, 
                foodService, orderService, waiterService, orderAppService);

            newOrderViewModel.RequestClose += () =>
            {
                this.Dispatcher.Invoke(() => this.Close());
            };
            DataContext = newOrderViewModel;
            InitializeComponent();
        }
    }
}
