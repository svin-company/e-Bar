
using eBar.WaiterApp.Service.Interfaces;

namespace eBar.WaiterApp.Commands
{
    public class OpenOrdersListCommand : BaseCommand
    {
        private readonly IDialogService _dialogService;
        private readonly IServiceProvider _serviceProvider;

        public OpenOrdersListCommand(IDialogService dialogService, IServiceProvider serviceProvider)
        {
            _dialogService = dialogService;
            _serviceProvider = serviceProvider;
        }

        public override void Execute(object parameter)
        {
            _dialogService.OpenOrdersListDialog(_serviceProvider);
        }
    }
}
