using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Exceptions
{
    public class ClassEmptyKeyAttributeException: Exception
    {
        public ClassEmptyKeyAttributeException() { }
        public ClassEmptyKeyAttributeException(string message): base (message) { }
    }
}
