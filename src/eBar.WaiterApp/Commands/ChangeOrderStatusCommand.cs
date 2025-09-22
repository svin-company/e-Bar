using eBar.WaiterApp.Model;
using eBar.WaiterApp.Storages;

namespace eBar.WaiterApp.Commands
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
