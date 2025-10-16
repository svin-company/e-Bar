namespace eBar.Core.Model
{
    public class OrderStatus
    {
        public int Id { get; }
        public string Name { get; }

        public OrderStatus(){}

        public OrderStatus(string name)
        {
            Name = name;
        }

        public OrderStatus(int id) => Id = id;

    }
    
}

