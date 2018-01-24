namespace MyServer.Server.Routing
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class InvalidOperationExcepton : Exception
    {
        public InvalidOperationExcepton()
        { }

        public InvalidOperationExcepton(string message) : base(message)
        { }

        public InvalidOperationExcepton(string message, Exception innerException) : base(message, innerException)
        { }

        protected InvalidOperationExcepton(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}