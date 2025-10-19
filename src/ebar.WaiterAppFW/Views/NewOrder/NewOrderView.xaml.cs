using eBar.Core.Model;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterAppFW.ViewModel;
using System.Windows;


namespace ebar.WaiterAppFW.Views.NewOrder
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

            int orderIsOpen = 1;
            var order = new Order(orderIsOpen);
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
