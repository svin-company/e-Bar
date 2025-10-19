using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;

namespace eBar.WaiterAppFW.Commands
{
    public class DeleteItemCommand : BaseCommand
    {
        public Order Order { get; set; }
        private readonly IOrderService _orderService;

        public bool CanExecute(object parameter) => parameter is OrderItem;

        public DeleteItemCommand(Order order, IOrderService orderService)
        {
            _orderService = orderService;
            Order = order;
        }

        public override void Execute(object parameter)
        {
            if (parameter is OrderItem orderItem)
            {
                _orderService.DeleteItem(Order, orderItem);
            }
        }
    }
}