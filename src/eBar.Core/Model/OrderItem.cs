using System.ComponentModel;

namespace eBar.Core.Model
{
    public class OrderItem : INotifyPropertyChanged
    {
        public int Id { get; }
        public Food Food { get; set; }
        public decimal TotalPrice => Food.Price * Amount;
        public int FoodId { get; set; }
        public int OrderId { get; set; }
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public OrderItem(int amout, int foodId, int orderId)
        {
            Amount = amout;
            FoodId = foodId;
            OrderId = orderId;
        }
        public OrderItem() { }
        public OrderItem(int id) => Id = id;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
