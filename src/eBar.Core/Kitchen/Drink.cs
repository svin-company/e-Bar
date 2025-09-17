namespace eBar.Core.Kitchen
{
    public class Drink : OrderPosition
    {
        public DrinkType Type {get; set;}

        public Drink(string name, DrinkType type)
        {
            Name = name;
            Type = type;
        }
    }
    
    public enum DrinkType
        {
        Non_alcohol,
        Alcohol
        }
}