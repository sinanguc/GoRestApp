using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace GorestApp.Core.Infrastructure.ErrorHandling
{
    [Serializable]
    public class RecordExistException : Exception
    {

        public RecordExistException() : base()
        { }

        public RecordExistException(String message) : base(message)
        { }

        public RecordExistException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        { }

        public RecordExistException(String message, Exception innerException) : base(message, innerException)
        { }

        protected RecordExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
