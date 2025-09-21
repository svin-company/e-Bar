using eBar.WaiterApp.Model;

namespace eBar.WaiterApp.Commands
{
    public class AddToOrderCommand : BaseCommand
    {
        public Order Order { get; set; }

        public bool CanExecute(object parameter) => parameter is Food;

        public AddToOrderCommand(Order order)
        {
            Order = order;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Food food)
            {
                Order.Add(food);
            }
        }
    }
}
