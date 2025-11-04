using eBar.Core.Model;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Service
{
    public class OrderAppService: IOrderAppService
    {
        public void DeleteItem(OrderViewModel order, OrderItemViewModel orderItem)
        {
            order.OrderItems.Remove(orderItem);
        }

        public void AddFood(OrderViewModel order, Food food)
        {
            if (order.OrderItems != null)
            {
                var existingOrderItem = order.OrderItems
                    .Where(x => x.Food.Name.Equals(food.Name))
                    .FirstOrDefault();
                if (existingOrderItem == null)
                {
                    var orderItem = new OrderItem
                    {
                        Id = 1,
                        Food = food,
                        Amount = 1
                    };
                    var orderItemVM = new OrderItemViewModel(orderItem);
                    order.OrderItems.Add(orderItemVM);
                }
                else
                {
                    existingOrderItem.Amount++;
                }
            }
            else
            {
                var orderItem = new OrderItem
                {
                    Id = 1,
                    Food = food,
                    Amount = 1
                };
                var orderItemVM = new OrderItemViewModel(orderItem);
                order.OrderItems.Add(orderItemVM);
            }
        }
    }
}
