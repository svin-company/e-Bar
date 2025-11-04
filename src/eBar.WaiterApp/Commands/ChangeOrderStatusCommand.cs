using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.ViewModel;
namespace eBar.WaiterApp.Commands
{
    public class ChangeOrderStatusCommand : BaseCommand
    {
        public bool CanExecute(object parameter) => parameter is OrderViewModel;
        private IOrderService _orderService;

        public ChangeOrderStatusCommand(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is OrderViewModel orderVM)
            {
                var updatedOrder = await _orderService.UpdateStatusAsync(orderVM.Order);
                orderVM.Order = updatedOrder;
            }
        }
    }
}
