namespace eBar.Core.Kitchen
{
    public class Food : OrderPosition
    {
        public FoodType Type {get; set;}

        public Food(string name, FoodType type)
        {
            Name = name;
            Type = type;
        }
    }
    
    public enum FoodType
    {
        Hot,
        Cold,
        Snacks
    }
}