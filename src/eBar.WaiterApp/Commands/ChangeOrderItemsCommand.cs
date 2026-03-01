
using eBar.DataStorage.Services.Interfaces;
using eBar.WaiterApp.Service.Interfaces;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Commands
{
    public class ChangeOrderItemsCommand: BaseCommand
    {
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        private readonly IServiceProvider _serviceProvider;

        public ChangeOrderItemsCommand(IOrderService orderService, IDialogService dialogService, IServiceProvider serviceProvider)
        {
            _orderService = orderService;
            _dialogService = dialogService;
            _serviceProvider = serviceProvider;
        }

        public override bool CanExecute(object parameter) => parameter is OrderViewModel;

        public override void Execute(object parameter)
        {
            if (parameter is OrderViewModel order)
            {
                _dialogService.ShowChangeOrderDialog(order, _serviceProvider);
            }
        }

        
    }
}
