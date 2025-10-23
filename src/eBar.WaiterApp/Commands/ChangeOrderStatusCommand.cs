using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
namespace eBar.WaiterApp.Commands
{
    public class ChangeOrderStatusCommand : BaseCommand
    {
        public bool CanExecute(object parameter) => parameter is Order;
        private IOrderService _orderService;

        public ChangeOrderStatusCommand(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is Order order)
            {
                var updatedOrder = await _orderService.UpdateStatusAsync(order);
                order.OrderStatusId = updatedOrder.OrderStatusId;
                order.IsOrderOpen = updatedOrder.IsOrderOpen;
            }
        }
    }
}
