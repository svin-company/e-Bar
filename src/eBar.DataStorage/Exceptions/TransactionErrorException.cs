using System;
using System.Collections.Generic;
using System.Text;

namespace eBar.DataStorage.Exceptions
{
    internal class TransactionErrorException : Exception
    {
        public TransactionErrorException()
        {
        }

        public TransactionErrorException(string message) : base(message)
        {
        }
    }
}
