using System.Collections.ObjectModel;

namespace eBar.Core.Model
{
    public class Table
    {
        public int Id { get; }
        public ObservableCollection <Order> Orders { get; set; }

        public Table()
        {
            Orders = new ObservableCollection<Order>();
        }
        
    }
}
