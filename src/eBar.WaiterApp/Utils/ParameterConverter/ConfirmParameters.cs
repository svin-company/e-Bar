using eBar.Core.Model;
using eBar.WaiterApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.WaiterApp.ParameterConverter
{
    public class ConfirmParameters
    {
        public TableViewModel Table { get; set; }
        public Waiter Waiter { get; set; }
    }
}
