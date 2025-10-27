using Kitchen=eBar.Core.Kitchen;
using RMQ = eBar.MessageBroker.Reader;
using eBar.MessageBroker.MessageConsumer;
using eBar.MessageBroker.MessageProducer;
using Moq;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services;
using Waiter = eBar.Core.Model;
using System.Threading.Tasks;

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
        var drink = new Kitchen.Drink("Cola", Kitchen.DrinkType.NonAlcohol);
        Assert.That("Cola" == drink.Name);

        var food = new Kitchen.Food("burgir", Kitchen.FoodType.Hot);
        Assert.That("burgir" == food.Name);
    }

    [Test]
    public async Task Broker_Test()
    {
        string testMessage = "Hello";
        var config = new RMQ.ConfigReader();
        IMessageConsumer consumer = new MessageConsumer(config);
        var consumerTask = consumer.GetMessageAsync("ebarTest");

        MessageProducer messageProducer = new(config);
        await messageProducer.SendMessageAsync("ebarExchange", "ebarTest", "qwerty", testMessage);

        Assert.That(testMessage, Is.EqualTo(await consumerTask));
    }

    public async Task Insert_Entity_Test()
    {
        // Arrange
        string testName = "Xoт-дог";
        decimal testPrice = 100;
        var foodRepoMock = new Mock<IFoodRepository>();
        foodRepoMock.Setup(x => x.AddAsync(testName, testPrice))
            .ReturnsAsync((string testName, decimal testPrice) => 1);
        var service = new FoodService(foodRepoMock.Object);

        //Act
        int id = await service.AddAsync(testName, testPrice);

        //Assert
        foodRepoMock.Verify(x => x.AddAsync(testName, testPrice), Times.Once);
        Assert.That(id, Is.EqualTo(1));
    }

    [Test]
    public async Task Update_Entity_Test()
    {
        //Arrange
        var foodRepoMock = new Mock<IFoodRepository>();
        var name = "Сэндвич";
        var newPrice = 100;
        var oldPrice = 300;
        foodRepoMock
            .Setup(x => x.GetAsync(name))
            .ReturnsAsync( new Waiter.Food 
            { 
                Id= 2, 
                Name = name, 
                Price = oldPrice 
            });

        foodRepoMock
            .Setup(x => x.UpdateAsync(It.IsAny<Waiter.Food>()))
            .Returns(Task.CompletedTask);
        var service = new FoodService(foodRepoMock.Object);

        //Act
        var food = await service.GetAsync(name);
        await service.UpdateAsync(name, newPrice);

        // Assert
        foodRepoMock.Verify(x => x.GetAsync(name), Times.Exactly(2));
        foodRepoMock.Verify(r => r.UpdateAsync(It.Is<Waiter.Food>(
            f => f.Id == food.Id &&
                    f.Name == food.Name &&
                    f.Price == newPrice
            )), Times.Once);

    }

    [Test]
    public async Task Delete_Entity_Test()
    {
        //Arrange
        string name = "Сэндвич";
        var foodRepoMock = new Mock<IFoodRepository>();
        foodRepoMock
            .Setup(x => x.GetAsync(name))
            .ReturnsAsync(new Waiter.Food
            {
                Id = 2,
                Name = "Сэндвич",
                Price = 300
            });
        foodRepoMock.Setup(x => x.DeleteAsync(2))
            .Returns(Task.CompletedTask);
        var service = new FoodService(foodRepoMock.Object);

        //Act
        var food = await service.GetAsync("Сэндвич");
        await service.DeleteAsync(food.Name);

        //Assert
        foodRepoMock.Verify(x => x.GetAsync(name), Times.Exactly(2));
        foodRepoMock.Verify(x => x.DeleteAsync(food.Id), Times.Once);

    }

}
