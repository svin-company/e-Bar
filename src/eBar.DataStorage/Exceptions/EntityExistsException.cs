using System;

namespace eBar.DataStorage.Exceptions
{
    public class EntityExistsException : Exception
    {
        public EntityExistsException(){}

        public EntityExistsException(string? message) : base(message) {}
    }
}
