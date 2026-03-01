
using eBar.WaiterApp.Commands;
using eBar.WaiterApp.Service.Interfaces;
using System.Windows.Input;

namespace eBar.WaiterApp.ViewModel
{
    public class HomeViewModel: ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IServiceProvider _serviceProvider;
        public ICommand CreateNewOrderCommand { get; }
        public ICommand OpenOrdersCommand { get; }

        public HomeViewModel(IDialogService dialogService, IServiceProvider serviceProvider)
        {
            _dialogService = dialogService;
            _serviceProvider = serviceProvider;
            CreateNewOrderCommand = new CreateOrderCommand(dialogService, serviceProvider);
            OpenOrdersCommand = new OpenOrdersListCommand(dialogService, serviceProvider);
        }
    }
}
