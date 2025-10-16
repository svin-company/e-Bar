using eBar.WaiterAppFW.Model;
using eBar.WaiterAppFW.Storages;

namespace eBar.WaiterAppFW.Commands
{
    public class ChangeOrderStatusCommand : BaseCommand
    {
        public bool CanExecute(object parameter) => parameter is Order;
        public override void Execute(object parameter)
        {
            if (parameter is Order order)
            {
                TableStorage.UpdateOrderStatus(order);
            }
        }
    }
}
