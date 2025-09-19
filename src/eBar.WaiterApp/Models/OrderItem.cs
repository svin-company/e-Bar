using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.Models
{
    public class OrderItem : INotifyPropertyChanged
    {
        public Guid Id { get; }
        public Food Food { get; set; }
        public decimal TotalPrice => Food.Price * Amount;

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

        public OrderItem(Food food, int amout)
        {
            Id = Guid.NewGuid();
            Food = food;
            Amount = amout;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
