using eBar.Core.Model;
using eBar.WaiterApp.Service;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Commands
{
    public class AddToOrderCommand : BaseCommand
    {
        public OrderViewModel Order { get; set; }
        private readonly IOrderAppService _orderAppService;

        public bool CanExecute(object parameter) => parameter is Food;

        public AddToOrderCommand(OrderViewModel order, IOrderAppService orderAppService)
        {
            Order = order;
            _orderAppService = orderAppService;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Food food)
            {
                _orderAppService.AddFood(Order, food);
            }
        }
    }
}
