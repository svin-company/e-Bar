using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace eBar.Core.Model
{
    public class Order : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();

        public DateTime OrderTime { get; set; } = DateTime.Now;

        private int _orderStatusId;
        private int _tableId;
        private string _statusName;

        public int OrderStatusId
        {
            get => _orderStatusId;
            set
            {
                if (_orderStatusId != value)
                {
                    _orderStatusId = value;
                    OnPropertyChanged(nameof(OrderStatusId));
                }
            }
        }
        public string OrderStatus
        {
            get => _statusName;
            set
            {
                if (_statusName != value)
                {
                    _statusName = value;
                    OnPropertyChanged(nameof(OrderStatus));
                }
            }
        }

        public int TableId
        {
            get => _tableId;
            set
            {
                if (_tableId != value)
                {
                    _tableId = value;
                    OnPropertyChanged(nameof(TableId));
                }
            }
        }
        public Order() { }
        public Order(int orderStatusId)
        {
            OrderStatusId = orderStatusId;
        }

        public Order(int id, int orderStatusId)
        {
            Id = id;
            OrderStatusId = orderStatusId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

