using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Exceptions
{
    public class NoRecordsException : Exception
    {
        public NoRecordsException()
        {
        }

        public NoRecordsException(string? message) : base(message)
        {
        }
    }
}
