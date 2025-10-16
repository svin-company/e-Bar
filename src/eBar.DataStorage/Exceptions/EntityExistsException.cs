using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Exceptions
{
    public class EntityExistsException : Exception
    {
        public EntityExistsException(){}

        public EntityExistsException(string? message) : base(message) {}
    }
}
