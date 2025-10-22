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
        private bool _isOrderOpen;

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
        public bool IsOrderOpen
        {
            get => _isOrderOpen;
            set
            {
                if (_isOrderOpen != value)
                {
                    _isOrderOpen = value;
                    OnPropertyChanged(nameof(IsOrderOpen));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

