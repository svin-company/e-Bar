using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Repositories;
using eBar.DataStorage.Services.Interfaces;
using eBar.DataStorage.Services;
using eBar.WaiterApp.ViewModel;
using System.Windows;
using eBar.Core.Model;

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

            var order = new Order
            {
                IsOrderOpen = true
            };
            var newOrderViewModel = new NewOrderViewModel(order, tableService, foodService, orderService);
            newOrderViewModel.RequestClose += () =>
            {
                this.Dispatcher.Invoke(() => this.Close());
            };
            DataContext = newOrderViewModel;
            InitializeComponent();
        }
    }
}
