using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Exceptions
{
    public class ClassEmptyColumnAttributeException: Exception
    {
        public ClassEmptyColumnAttributeException() { }
        public ClassEmptyColumnAttributeException(string message) : base(message){ }
    }
}
