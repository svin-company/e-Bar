using eBar.Core.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace eBar.WaiterApp.ViewModel
{
    public class OrderViewModel: ViewModelBase
    {
        private Order _order;

        public Order Order 
        {
            get => _order; 
            set 
            {
                if (Order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(Order));
                    OnPropertyChanged(nameof(IsOrderOpen));
                    OnPropertyChanged(nameof(OrderStatusId));
                }
            }  
        }

        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; }
        public int Id => Order.Id;
        public DateTime OrderTime => Order.OrderTime;

        public OrderViewModel(Order order)
        {
            this.Order = order;
            ObservableCollection<OrderItemViewModel> orderViewModels = new();
            OrderItems = [];

            foreach (var item in this.Order.OrderItems)
            {
                var orderItemVM = new OrderItemViewModel(item);
                OrderItems.Add(orderItemVM);
            }

            OnPropertyChanged(nameof(OrderItems));
        }
        public string WaiterName
        {
            get => Order.WaiterName;
            set
            {
                if (Order.WaiterName != value)
                    Order.WaiterName = value;
                OnPropertyChanged(nameof(WaiterName));
            }
        }

        public int WaiterId
        {
            get => Order.WaiterId;
            set
            {
                if (Order.WaiterId != value)
                {
                    Order.WaiterId = value;
                    OnPropertyChanged(nameof(WaiterId));
                }
            }
        }

        public int OrderStatusId
        {
            get => Order.OrderStatusId;
            set
            {
                if (Order.OrderStatusId != value)
                {
                    Order.OrderStatusId = value;
                    OnPropertyChanged(nameof(OrderStatusId));
                }
            }
        }
        public bool IsOrderOpen
        {
            get => Order.IsOrderOpen;
            set
            {
                if (Order.IsOrderOpen != value)
                {
                    Order.IsOrderOpen = value;
                    OnPropertyChanged(nameof(IsOrderOpen));
                }
            }
        }

        public int TableId
        {
            get => Order.TableId;
            set
            {
                if (Order.TableId != value)
                {
                    Order.TableId = value;
                    OnPropertyChanged(nameof(TableId));
                }
            }
        }
    }
}
