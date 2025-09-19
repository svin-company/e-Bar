using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.Models
{
    public class Food
    {
        public Guid Id { get;}
        public string Name { get;}
        public decimal Price { get; set; }
        public Food(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }
    }
}
