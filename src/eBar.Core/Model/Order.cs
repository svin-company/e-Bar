using System;
using System.Collections.Generic;
namespace eBar.Core.Model
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public DateTime OrderTime { get; set; }
        public string WaiterName { get; set; }
        public int WaiterId { get; set; }
        public int OrderStatusId { get; set; }
        public bool IsOrderOpen { get; set; }
        public int TableId { get; set; }
    }
}

