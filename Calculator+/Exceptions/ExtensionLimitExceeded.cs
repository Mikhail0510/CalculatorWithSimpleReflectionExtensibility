using System;
using System.Runtime.Serialization;

namespace Calculator_
{
    [Serializable]
    internal class ExtensionLimitExceeded : Exception
    {
        public ExtensionLimitExceeded()
        {
        }

        public ExtensionLimitExceeded(string? message) : base(message)
        {
        }

        public ExtensionLimitExceeded(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExtensionLimitExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}