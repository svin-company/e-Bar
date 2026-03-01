using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.ViewModel;
using System.Windows;

namespace eBar.WaiterApp.Commands
{

    public class DeleteOrderCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => parameter is OrderViewModel;
        private readonly IOrderService _orderService;
        private readonly OrderListViewModel _orderListViewModel;

        public DeleteOrderCommand(IOrderService orderService, OrderListViewModel orderListViewModel)
        {
            _orderService = orderService;
            _orderListViewModel = orderListViewModel;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is OrderViewModel orderVM)
            {
                var result = await _orderService.DeleteOrderAsync(orderVM.Order);

                if (result)
                    _orderListViewModel.OrdersForSelectedTable.Remove(orderVM);

                else
                    MessageBox.Show("Не удалось удалить заказ");
            }
        }
    }
}
