using System.Collections.ObjectModel;
using System.ComponentModel;
namespace eBar.Core.Model
{
    public partial class Order : INotifyPropertyChanged
    {
        public int Id { get; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }
        public DateTime OrderTime { get; }
        private int _orderStatusId;
        private int _tableId;

        public int StatusId
        {
            get => _orderStatusId;
            set
            {
                if (_orderStatusId != value)
                {
                    _orderStatusId = value;
                    OnPropertyChanged(nameof(_orderStatusId));
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
                    OnPropertyChanged(nameof(_tableId));
                }
            }
        }

        public Order(DateTime orderTime, int orderStatusId, int tableId)
        {
            OrderItems = new ObservableCollection<OrderItem>();
            OrderTime = orderTime;
            _orderStatusId = orderStatusId;
            _tableId = tableId;
        }
        public Order(int id) => Id = id;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

