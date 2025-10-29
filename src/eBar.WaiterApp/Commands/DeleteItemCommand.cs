using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.Service;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Commands
{
    public class DeleteItemCommand : BaseCommand
    {
        public OrderViewModel Order { get; set; }
        private readonly IOrderAppService _orderService;

        public bool CanExecute(object parameter) => parameter is OrderItemViewModel;

        public DeleteItemCommand(OrderViewModel order, IOrderAppService orderService)
        {
            _orderService = orderService;
            Order = order;
        }

        public override void Execute(object parameter)
        {
            if (parameter is OrderItemViewModel orderItem)
            {
                _orderService.DeleteItem(Order, orderItem);
            }
        }
    }
}