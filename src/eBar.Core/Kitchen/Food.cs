namespace eBar.Core.Kitchen
{
    public class Food : OrderPosition
    {
        public string Name {get; set;}
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
        snacks
    }
}