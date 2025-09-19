using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.Models
{
    public class Order : INotifyPropertyChanged
    {
        private static int _generatedId = 0;
        public int Id { get; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }
        public DateTime OrderTime { get; }
        private OrderStatus _status;

        public OrderStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public Order()
        {
            Id = Interlocked.Increment(ref _generatedId);
            OrderItems = new();
            OrderTime = DateTime.Now;
            Status = OrderStatus.Open;
        }

        public enum OrderStatus
        {
            Open,
            Closed
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Add(Food food)
        {
            OrderItem existingItem = this.OrderItems.Where(x => x.Food.Id == food.Id).FirstOrDefault();
            if (existingItem == null)
            {
                this.OrderItems.Add(new OrderItem(food, 1));
            }
            else
            {
                existingItem.Amount++;
            }

        }

        internal void Delete(OrderItem orderItem)
        {
            OrderItem existingItem = this.OrderItems.Where(x => x.Id == orderItem.Id).FirstOrDefault();
            if (existingItem !=null)
            {
                this.OrderItems.Remove(existingItem);
            }
        }
    }
}

