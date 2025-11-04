
namespace eBar.Core.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Food Food { get; set; }
        public decimal TotalPrice => Food.Price * Amount;
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}
