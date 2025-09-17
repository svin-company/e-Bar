using eBar.Core.Kitchen;

namespace eBar.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MainTest()
    {
        Assert.Pass();
    }

    [Test]
    public void Order_Test()
    {
        var drink = new Drink("Cola", DrinkType.non_alcohol);
        Assert.Equals("Cola", drink.Name);

        var food = new Food("burgir", FoodType.Hot);
        Assert.Equals("burgir", food.Name);
    }
}