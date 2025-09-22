using eBar.WaiterApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.Storages
{
    public static class FoodStorage
    {
        public static readonly ObservableCollection<Food> foods =
        [
            new Food("Лобстер", 3000),
            new Food("Доширак", 300),
            new Food("Креветки в кляре", 1000),
            new Food("Карбонара", 600),
            new Food("Пицца пеперони", 750),
            new Food("Пиво", 200)
        ];

        public static ObservableCollection<Food> GetAll() => foods;
    }
}
