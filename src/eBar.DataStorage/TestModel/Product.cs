using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.TestModel
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("image_path")]
        public string? ImagePath { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("product_category_id")]
        public int ProductCategoryId { get; set; }

        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        public Product() {}
        public Product(string name, decimal price, string imagePath, string description, int productCategoryId, int restaurantId)
        {
            Name = name;
            Price = price;
            ImagePath = imagePath;
            Description = description;
            ProductCategoryId = productCategoryId;
            RestaurantId = restaurantId;

        }

    }
}
