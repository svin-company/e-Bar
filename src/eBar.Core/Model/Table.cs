using System.Collections.Generic;

namespace eBar.Core.Model
{
    public class Table
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
