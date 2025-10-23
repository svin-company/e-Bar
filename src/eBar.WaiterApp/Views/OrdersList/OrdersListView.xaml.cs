using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Repositories;
using eBar.DataStorage.Services.Interfaces;
using eBar.DataStorage.Services;
using eBar.WaiterApp.ViewModel;
using System.Windows;


namespace eBar.WaiterApp.Views.OrdersList
{
    public partial class OrdersListView : Window
    {
        public OrdersListView()
        {
            var reader = new ConfigReader();

            ITableRepository tableRepository = new TableRepository(reader);
            ITableService tableService = new TableService(tableRepository);

            IOrderItemRepository orderItemRepository = new OrderItemRepository(reader);
            IOrderRepository orderRepository = new OrderRepository(reader);
            IOrderService orderService = new OrderService(orderRepository, orderItemRepository);

            DataContext = new OrderListViewModel(tableService, orderService);
            InitializeComponent();
        }
    }
}
