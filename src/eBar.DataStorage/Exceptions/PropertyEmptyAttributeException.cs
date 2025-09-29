using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Exceptions
{
    public class PropertyEmptyAttributeException: Exception
    {
        public PropertyEmptyAttributeException() { }
        public PropertyEmptyAttributeException(string message): base (message) { }
    }
}
