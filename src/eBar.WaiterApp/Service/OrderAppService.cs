using eBar.Core.Model;
using eBar.WaiterApp.Service.Interfaces;
using eBar.WaiterApp.ViewModel;

namespace eBar.WaiterApp.Service
{
    public class OrderAppService: IOrderAppService
    {
        public void DeleteItem(OrderViewModel order, OrderItemViewModel orderItem)
        {
            order.OrderItems.Remove(orderItem);
            order.Order.OrderItems.Remove(orderItem.OrderItem);
        }

        public void AddFood(OrderViewModel order, Food food)
        {
            if (order.OrderItems != null)
            {
                var existingOrderItemVM = order.OrderItems
                    .Where(x => x.Food.Name.Equals(food.Name))
                    .FirstOrDefault();
                var existingOrderItemModel = order.Order.OrderItems
                    .Where(x => x.Food.Name.Equals(food.Name))
                    .FirstOrDefault();
                if (existingOrderItemVM == null)
                {
                    var orderItem = new OrderItem
                    {
                        Id = 1,
                        Food = food,
                        FoodId = food.Id,
                        Amount = 1,
                        OrderId = order.Id,
                    };
                    var orderItemVM = new OrderItemViewModel(orderItem);
                    order.OrderItems.Add(orderItemVM);
                    order.Order.OrderItems.Add(orderItem);
                }
                else
                {
                    existingOrderItemVM.Amount++;
                }
            }
            else
            {
                var orderItem = new OrderItem
                {
                    Id = 1,
                    Food = food,
                    FoodId = food.Id,
                    Amount = 1,
                    OrderId = order.Id,
                };
                var orderItemVM = new OrderItemViewModel(orderItem);
                order.OrderItems.Add(orderItemVM);
                order.Order.OrderItems.Add(orderItem);
            }
        }
    }
}
