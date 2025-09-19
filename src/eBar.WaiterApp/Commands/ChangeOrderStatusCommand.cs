using eBar.WaiterApp.Models;
using eBar.WaiterApp.Storages;

namespace eBar.WaiterApp.Commands
{
    internal class ChangeOrderStatusCommand : BaseCommand
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
