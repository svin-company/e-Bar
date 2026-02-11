using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eBar.WaiterApp.Commands
{

    public class DeleteOrderCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => parameter is OrderViewModel;
        private IOrderService _orderService;

        public DeleteOrderCommand(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async override void Execute(object parameter)
        {
            if (parameter is OrderViewModel orderVM)
            {
                var result = await _orderService.DeleteOrderAsync(orderVM.Order).ConfigureAwait(false);
                if (!result)
                {
                    MessageBox.Show("Не удалось удалить заказ");
                }
            }
        }
    }
}
