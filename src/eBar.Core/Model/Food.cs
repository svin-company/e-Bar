namespace eBar.Core.Model
{
    public class Food
    {
        public  int Id { get; set; }
        public string Name { get;}
        public decimal Price { get; set; }

        public Food() { }
        public Food(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Food(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
