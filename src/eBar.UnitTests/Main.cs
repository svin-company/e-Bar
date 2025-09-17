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
        var drink = new Drink("Cola", DrinkType.Non_alcohol);
        Assert.That("Cola" == drink.Name);

        var food = new Food("burgir", FoodType.Hot);
        Assert.That("burgir" == food.Name);
    }
}