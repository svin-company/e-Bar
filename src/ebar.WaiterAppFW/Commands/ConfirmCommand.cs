using eBar.Core.Model;
using eBar.DataStorage.Services.Interfaces;
using System;
using System.Net.Http;
using System.Windows;

namespace eBar.WaiterAppFW.Commands
{
    public class ConfirmCommand: BaseCommand
    {
        public Order Order { get; set; }
        private readonly IOrderService _orderService;
        public bool CanExecute(object parameter) => parameter is Table;

        private readonly Action _onConfirmed;

        public ConfirmCommand(Order order, IOrderService orderService, Action onConfirmed)
        {
            Order = order;
            _orderService = orderService;
            _onConfirmed = onConfirmed;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is Table table)
            {
                bool result = await _orderService.AddOrderAsync(Order, table.Id).ConfigureAwait(false);
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
