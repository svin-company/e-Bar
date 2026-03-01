using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;


namespace eBar.WaiterApp.Commands
{
    public class ConfirmOrderUpdateCommand : BaseCommand
    {
        private readonly IOrderService _orderService;
        private readonly Action _onConfirmed;
        public OrderViewModel OrderVM { get; set; }

        public ConfirmOrderUpdateCommand(OrderViewModel order, IOrderService orderService,
            Action onConfirmed)
        {
            _onConfirmed = onConfirmed;
            OrderVM = order;
            _orderService = orderService;
        }

        public override bool CanExecute(object parameter) => parameter is OrderViewModel;
        public override async void Execute(object parameter)
        {
            if (parameter is OrderViewModel orderVM)
            {
                var updatedItems = await _orderService.UpdateOrderItemsAsync(OrderVM.Order);
                if (updatedItems != null)
                {
                    orderVM.Order.OrderItems = updatedItems.ToList();
                    _onConfirmed?.Invoke();
                }
                else
                {
                    MessageBox.Show("Ошибка обновления заказа");
                }
                
            }
        }
    }
}
