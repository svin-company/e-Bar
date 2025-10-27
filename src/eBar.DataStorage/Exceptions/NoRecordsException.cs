using System;
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
