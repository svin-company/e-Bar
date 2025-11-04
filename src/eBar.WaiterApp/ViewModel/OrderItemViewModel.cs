using eBar.Core.Model;

namespace eBar.WaiterApp.ViewModel
{
    public class OrderItemViewModel: ViewModelBase
    {
        public OrderItem OrderItem { get; set; }

        public OrderItemViewModel(OrderItem orderItem)
        {
            OrderItem = orderItem;
        }

        public int Id => OrderItem.Id;
        public Food Food => OrderItem.Food;
        public decimal TotalPrice => OrderItem.TotalPrice;
        public int OrderId => OrderItem.OrderId;

        public int Amount
        {
            get => OrderItem.Amount;
            set
            {
                if (OrderItem.Amount != value)
                {
                    OrderItem.Amount = value;
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
    }
}
