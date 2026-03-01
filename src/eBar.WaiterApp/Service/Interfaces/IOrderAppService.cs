using eBar.Core.Model;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Service.Interfaces
{
    public interface IOrderAppService
    {
        public void AddFood(OrderViewModel order, Food food);
        public void DeleteItem(OrderViewModel order, OrderItemViewModel orderItem);
    }
}
