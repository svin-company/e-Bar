using Dapper;
using eBar.Core.Kitchen;
using eBar.DataStorage.ConfigReader;
using eBar.DataStorage.Providers.EntityAttributeProvider;
using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.TestModel;
using Npgsql;

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

    [Test]
    public async Task Insert_Entity_Test()
    {
        //Arrange: Создаем запись для добавления в таблицу
        var testProduct = new Product("test", 111, "imagePathTest", "test", 1, 1);

        //Act: Делаем запись в БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var  productId = await sqlConnection.AddAsync(testProduct);

        //Assert: Проверяем, что запись появилась в БД и удаляем ее
        var insertedProduct = await GetRecord(productId);

        AssertProductMatch(insertedProduct, testProduct);
        DeleteRecord(productId);
    }

    [Test]
    public async Task Update_Entity_Test()
    {
        //Arrange: Создаем запись, которую будем менять и получаем его id
        // Проверяем, что продукт создался
        var testProduct = new Product("test", 111, "imagePathTest", "test", 1, 1);
        int productId = await CreateRecord(testProduct);
        var productToUpdate = await GetRecord(productId);

        //Act: Вносим изменения в БД
        testProduct.Name = productToUpdate.Name = "new name";
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        bool result = await sqlConnection.UpdateAsync(productToUpdate);

        //Assert: Проверяем, что изменения произошли
        AssertProductMatch(productToUpdate, testProduct);
        DeleteRecord(productId);
    }

    [Test]
    public async Task Select_Entity_Test()
    {
        //Arrange: Создаем продукт и получаем его id
        var testProduct = new Product("test", 111, "imagePathTest", "test", 1, 1);
        int productId = await CreateRecord(testProduct);

        //Act: Извлекаем данные из БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var selectedProduct = await sqlConnection.GetByIdAsync(productId);

        //Assert: Проверяем, что данные извлекаются корректно и в полном объеме, затем удаляем запись
        AssertProductMatch(selectedProduct, testProduct);
        DeleteRecord(productId);
    }

    [Test]
    public async Task Delete_Entity_Test()
    {
        //Arrange: Создаем продукт, который хотим удалить и находим его id
        var testProduct = new Product("test", 111, "imagePathTest", "test", 1, 1);
        int productId = await CreateRecord(testProduct);

        //Act: Удаляем запись из БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        await sqlConnection.DeleteAsync(productId);

        //Assert: Проверяем, что запись действительно удалена
        var deleted = await sqlConnection.GetByIdAsync(productId);

        Assert.That(deleted, Is.Null);
    }

    private static void AssertProductMatch(Product basicProduct, Product testProduct)
    {
        Assert.That(basicProduct, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(basicProduct.Name, Is.EqualTo(testProduct.Name));
            Assert.That(basicProduct.Price, Is.EqualTo(testProduct.Price));
            Assert.That(basicProduct.RestaurantId, Is.EqualTo(testProduct.RestaurantId));
            Assert.That(basicProduct.Description, Is.EqualTo(testProduct.Description));
            Assert.That(basicProduct.ProductCategoryId, Is.EqualTo(testProduct.ProductCategoryId));
            Assert.That(basicProduct.ImagePath, Is.EqualTo(testProduct.ImagePath));
        });
    }

    private static async Task<int> CreateRecord(Product product)
    {
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        int id = await sqlConnection.AddAsync(product);
        return id;
    }

    private static async Task<bool> DeleteRecord(int id)
    {
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var result = await sqlConnection.DeleteAsync(id);
        return result;
    }

    private static async Task<Product> GetRecord(int id)
    {
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var product = await sqlConnection.GetByIdAsync(id);
        return product;
    }
}
