using eBar.WaiterApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace eBar.WaiterApp.Storages
{
    class TableStorage
    {
        public static readonly ObservableCollection<Table> tables = CreateTables();

        private static ObservableCollection<Table> CreateTables()
        {
            ObservableCollection<Table> tables = new ObservableCollection<Table>();
            for (int i = 0; i < 5; i++)
            {
                tables.Add(new Table());
            }
            return tables;
        }



        public static ObservableCollection<Table> GetAll() => tables;


        public static void UpdateOrderStatus(Order order)
        {
            var table =  tables.FirstOrDefault(t => t.Orders.Any(o => o.Id == order.Id));
            var existtingOrder = table.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (existtingOrder.Status == Order.OrderStatus.Open)
            {
                existtingOrder.Status = Order.OrderStatus.Closed;
            }
            else
            {
                existtingOrder.Status=Order.OrderStatus.Open;
            }
        }

        internal static bool SaveOrder(Table table, Order order)
        {
            if (order.OrderItems.Count == 0)
            {
                MessageBox.Show("Пустой заказ!");
                return false;
            }

            var existingTable = tables.Where(x => x.Id == table.Id).FirstOrDefault();

            if (existingTable != null && existingTable.Orders.Count !=0 && HasOpenOrder(existingTable))
            {
                MessageBox.Show($"Стол {existingTable.Id} уже имеет незакрытый заказ");
                return false;
            }

            existingTable.Orders.Add(order);
            return true;
        }

        private static bool HasOpenOrder(Table table)
        {
            return  table.Orders.Any(x => x.Status == Order.OrderStatus.Open);

        }
    }
}
