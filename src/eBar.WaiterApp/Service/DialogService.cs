using eBar.WaiterApp.Service.Interfaces;
using eBar.WaiterApp.ViewModel;
using eBar.WaiterApp.Views.ChangeOrder;
using eBar.WaiterApp.Views.NewOrder;
using eBar.WaiterApp.Views.OrdersList;
using Microsoft.Extensions.DependencyInjection;

namespace eBar.WaiterApp.Service
{
    public class DialogService : IDialogService
    {

        public void ShowChangeOrderDialog(OrderViewModel order, IServiceProvider provider)
        {
            var changeOrderVM = ActivatorUtilities.CreateInstance<ChangeOrderViewModel>(
                provider,
                order
             );

            _ = new ChangeOrderView(changeOrderVM).ShowDialog();
        }

        public void CreateOrderDialog(IServiceProvider provider)
        {
            _ = provider.GetRequiredService<NewOrderView>().ShowDialog();
        }

        public void OpenOrdersListDialog(IServiceProvider provider)
        {
            _ = provider.GetRequiredService<OrdersListView>().ShowDialog();
        }
    }
}
