using eBar.WaiterApp.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace eBar.WaiterApp.Storage
{
    class TableStorage
    {
        public static readonly ObservableCollection<Table> tables = CreateTables();

        private static ObservableCollection<Table> CreateTables()
        {
            int tableCount = 5;
            ObservableCollection<Table> tables = new ObservableCollection<Table>();
            for (int i = 0; i < tableCount; i++)
            {
                tables.Add(new Table());
            }
            return tables;
        }



        public static ObservableCollection<Table> GetAll() => tables;


        public static void UpdateOrderStatus(Order order)
        {
            var table =  tables.FirstOrDefault(t => t.Orders.Any(o => o.Id == order.Id));
            var existingOrder = table.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (existingOrder.Status == Order.OrderStatus.Open)
            {
                existingOrder.Status = Order.OrderStatus.Closed;
            }
            else
            {
                existingOrder.Status=Order.OrderStatus.Open;
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
