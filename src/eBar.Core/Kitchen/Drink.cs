namespace eBar.Core.Kitchen
{
    public class Drink : OrderPosition
    {
        public string Name {get; set;}
        public DrinkType Type {get; set;}

        public Drink(string name, DrinkType type)
        {
            Name = name;
            Type = type;
        }
    }
    
    public enum DrinkType
        {
        non_alcohol,
        alcohol
        }
}