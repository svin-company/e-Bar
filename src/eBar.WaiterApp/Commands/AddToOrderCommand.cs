using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;

namespace eBar.WaiterApp.Commands
{
    public class AddToOrderCommand : BaseCommand
    {
        public Order Order { get; set; }
        private readonly IOrderService _orderService;

        public bool CanExecute(object parameter) => parameter is Food;

        public AddToOrderCommand(Order order, IOrderService orderService)
        {
            Order = order;
            _orderService = orderService;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Food food)
            {
                _orderService.AddFood(Order, food);
            }
        }
    }
}
