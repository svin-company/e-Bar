using System.ComponentModel;

namespace eBar.Core.Model
{
    public class OrderItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Food Food { get; set; }
        public decimal TotalPrice => Food.Price * Amount;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
