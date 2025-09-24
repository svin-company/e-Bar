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
        var insertedProduct = new Product("thirdTest", 111, "imagePathTest", "test", 1, 1);

        //Act: Делаем запись в БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        bool result = await sqlConnection.AddAsync(insertedProduct);

        //Assert: Проверяем, что запись появилась в БД
        using var connection = new NpgsqlConnection(new ConfigReader().GetConnectionString());
        await connection.OpenAsync();
        var testProduct = connection.QueryAsync<Product>
           ($@"SELECT name AS Name, price AS Price, image_path AS ImagePath, 
                    description AS Description, product_category_id as ProductCategoryId, 
                    restaurant_id AS RestaurantId FROM product as Product 
                    WHERE name = @Name", new { insertedProduct.Name }).Result.FirstOrDefault();

        AssertProductMatch(insertedProduct, testProduct); 
    }

    [Test]
    public async Task Update_Entity_Test()
    {
        //Arrange: Находим запись, которую хотим изменить
        int productId = 81;
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var updatedProduct = await sqlConnection.GetByIdAsync(productId);
        updatedProduct.Name = "new name";

        //Act: Вносим изменения в БД
        bool result = await sqlConnection.UpdateAsync(updatedProduct);

        //Assert: Проверяем, что строка в БД изменилась
        using var connection = new NpgsqlConnection(new ConfigReader().GetConnectionString());
        await connection.OpenAsync();
        var testProduct = connection.QueryAsync<Product>
           ($@"SELECT name AS Name, price AS Price, image_path AS ImagePath, 
                    description AS Description, product_category_id as ProductCategoryId, 
                    restaurant_id AS RestaurantId FROM product as Product 
                    WHERE id = @Id", new { Id = productId }).Result.FirstOrDefault();

        AssertProductMatch(updatedProduct, testProduct);
    }

    [Test]
    public async Task Select_Entity_Test()
    {
        //Arrange: Выбираем id продукта, который хотим найти
        int productId = 81;

        //Act: Извлекаем данные из БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        var selectedProduct = await sqlConnection.GetByIdAsync(productId);

        //Assert: Проверяем, что данные извлекаются корректно и в полном объеме
        using var connection = new NpgsqlConnection(new ConfigReader().GetConnectionString());
        await connection.OpenAsync();
        var testProduct = connection.QueryAsync<Product>
            ($@"SELECT name AS Name, price AS Price, image_path AS ImagePath, 
                    description AS Description, product_category_id as ProductCategoryId, 
                    restaurant_id AS RestaurantId FROM product as Product 
                    WHERE id = @Id", new { Id = productId }).Result.FirstOrDefault();

        AssertProductMatch(selectedProduct, testProduct);
    }

    [Test]
    public async Task Delete_Entity_Test()
    {
        //Arrange: Выбираем id продукта, который хотим удалить
        int productId = 2;

        //Act: Удаляем запись из БД
        var sqlConnection = new SqlConnectionProvider<Product>(new ConfigReader(), new EntityAttributeProvider());
        await sqlConnection.DeleteAsync(productId);

        //Assert: Проверяем, что запись действительно удалена
        using var connection = new NpgsqlConnection(new ConfigReader().GetConnectionString());
        var deleted = await connection.QueryFirstOrDefaultAsync<int?>(
            "SELECT id AS Id FROM product AS Product WHERE id = @Id", new { Id = productId });

        Assert.That(deleted, Is.Null);
    }

    private static void AssertProductMatch(Product basicProduct, Product testProduct)
    {
        Assert.That(testProduct, Is.Not.Null);
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
}