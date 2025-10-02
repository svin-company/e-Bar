using eBar.Core.Kitchen;
using eBar.DataStorage.ConfigReader;
using eBar.DataStorage.Providers.EntityAttributeProvider;
using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.TestModel;
using eBar.MessageBroker.ConfigReader.ConfigReader;
using eBar.MessageBroker.MessageConsumer;
using eBar.MessageBroker.MessageProducer;
using System.Formats.Asn1;

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
        var drink = new Drink("Cola", DrinkType.NonAlcohol);
        Assert.That("Cola" == drink.Name);

        var food = new Food("burgir", FoodType.Hot);
        Assert.That("burgir" == food.Name);
    }
}