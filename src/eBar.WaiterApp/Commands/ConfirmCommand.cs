using eBar.Core.Model;
using eBar.Core.ParameterConverter;
using eBar.DataStorage.Services.Interfaces;
using System.Windows;

namespace eBar.WaiterApp.Commands
{
    public class ConfirmCommand: BaseCommand
    {
        public Order Order { get; set; }
        private readonly IOrderService _orderService;
        public bool CanExecute(object parameter) => parameter is ConfirmParameters;

        private readonly Action _onConfirmed;

        public ConfirmCommand(Order order, IOrderService orderService, Action onConfirmed)
        {
            Order = order;
            _orderService = orderService;
            _onConfirmed = onConfirmed;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is ConfirmParameters param)
            {
                bool result = await _orderService.
                    AddOrderAsync(Order, param.Table.Id, param.Waiter.Id).ConfigureAwait(false);
                if (!result)
                {
                    MessageBox.Show("Ошибка! Заказ пуст");
                } 
                else
                    _onConfirmed?.Invoke();
            }
        }
    }
}
