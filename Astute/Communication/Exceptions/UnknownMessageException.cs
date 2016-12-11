using System;

namespace Astute.Communication.Exceptions
{
    public class UnknownMessageException : NotSupportedException
    {
        public UnknownMessageException(string message) : base(message)
        {
        }
    }
}