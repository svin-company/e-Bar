using System.Collections.ObjectModel;

namespace eBar.WaiterApp.Models
{
    public class Table
    {
        private static int _generatedId = 0;

        public int Id { get; }
        public ObservableCollection <Order> Orders { get; set; }

        public Table()
        {
            Id = Interlocked.Increment(ref _generatedId);
            Orders = new();
        }
        
    }
}
