using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.ParameterConverter;
using eBar.WaiterApp.ViewModel;
using System.Windows;

namespace eBar.WaiterApp.Commands
{
    public class ConfirmCommand: BaseCommand
    {
        public OrderViewModel Order { get; set; }
        private readonly IOrderService _orderService;
        public bool CanExecute(object parameter) => parameter is ConfirmParameters;

        private readonly Action _onConfirmed;

        public ConfirmCommand(OrderViewModel order, IOrderService orderService, Action onConfirmed)
        {
            Order = order;
            _orderService = orderService;
            _onConfirmed = onConfirmed;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is ConfirmParameters param)
            {
                var orderItems = new List<OrderItem>();
                foreach (var item in Order.OrderItems)
                {
                    orderItems.Add(item.OrderItem);
                }
                var order = Order.Order;
                order.OrderItems = orderItems;

                bool result = await _orderService.
                    AddOrderAsync(order, param.Table.Id, param.Waiter.Id).ConfigureAwait(false);
                if (!result)
                {
                    MessageBox.Show("Ошибка! Заказ пуст");
                }
                else
                {
                    Order.Order = order;
                    _onConfirmed?.Invoke();
                }
                    
            }
        }
    }
}
