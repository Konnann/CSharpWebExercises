namespace MyServer.Server.Exceptions
{
    using System;

    public class InvalidResponseException : Exception
    {
        public InvalidResponseException()
        { }

        public InvalidResponseException(string message) : base(message)
        { }
    }
}
