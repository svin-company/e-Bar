using eBar.WaiterApp.Models;
using eBar.WaiterApp.Storages;

namespace eBar.WaiterApp.Commands
{
    public class ConfirmCommand: BaseCommand
    {
        public Order Order { get; set; }

        public bool CanExecute(object parameter) => parameter is Table;

        private readonly Action _onConfirmed;

        public ConfirmCommand(Order order, Action onConfirmed)
        {
            Order = order;
            _onConfirmed = onConfirmed;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Table table)
            {
                bool result = TableStorage.SaveOrder(table, Order);
                if (result) _onConfirmed?.Invoke();

            }
        }
    }
}
