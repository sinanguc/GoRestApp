using System;
using System.Runtime.Serialization;

namespace GorestApp.Core.Infrastructure.ErrorHandling
{
    [Serializable]
    public class DatabaseException : Exception
    {
        public DatabaseException() : base()
        { }

        public DatabaseException(String message) : base(message)
        { }

        public DatabaseException(String message, Exception innerException) : base(message, innerException)
        { }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

    }
}
