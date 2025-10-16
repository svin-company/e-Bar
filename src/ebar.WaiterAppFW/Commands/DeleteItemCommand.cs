using eBar.WaiterAppFW.Model;

namespace eBar.WaiterAppFW.Commands
{
    public class DeleteItemCommand : BaseCommand
    {
        public Order Order { get; set; }

        public bool CanExecute(object parameter) => parameter is OrderItem;

        public DeleteItemCommand(Order order)
        {
            Order = order;
        }

        public override void Execute(object parameter)
        {
            if (parameter is OrderItem orderItem)
            {
                Order.Delete(orderItem);
            }
        }
    }
}