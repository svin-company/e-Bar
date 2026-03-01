using eBar.Core.Model;
using eBar.WaiterApp.Service.Interfaces;
using eBar.WaiterApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.Commands
{
    public class CreateOrderCommand : BaseCommand
    {

        private readonly IDialogService _dialogService;
        private readonly IServiceProvider _serviceProvider;

        public CreateOrderCommand(IDialogService dialogService, IServiceProvider serviceProvider)
        {
            _dialogService = dialogService;
            _serviceProvider = serviceProvider;
        }

        public override void Execute(object parameter)
        {
            _dialogService.CreateOrderDialog(_serviceProvider);
        }
    } 
}
