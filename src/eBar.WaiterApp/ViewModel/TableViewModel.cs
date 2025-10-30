using eBar.Core.Model;
using System.Collections.ObjectModel;

namespace eBar.WaiterApp.ViewModel
{
    public class TableViewModel: ViewModelBase
    {
        private readonly Table _table;
        public int Id => _table.Id;
        public ObservableCollection<Order> Orders;

        public TableViewModel(Table table)
        {
            _table = table;
            Orders = new ObservableCollection<Order>(_table.Orders);
            OnPropertyChanged(nameof(Orders));
        }
    }
}
