using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Service.Interfaces
{
    public interface IDialogService
    {
        public void ShowChangeOrderDialog(OrderViewModel order, IServiceProvider provider);

        public void CreateOrderDialog(IServiceProvider provider);

        public void OpenOrdersListDialog(IServiceProvider provider);
    }
}
