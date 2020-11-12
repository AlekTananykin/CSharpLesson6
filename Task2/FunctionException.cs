using System;
using System.Runtime.Serialization;

namespace Task2
{
    [Serializable]
    internal class FunctionException : Exception
    {
        public FunctionException()
        {
        }

        public FunctionException(string message) : base(message)
        {
        }

        public FunctionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FunctionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}