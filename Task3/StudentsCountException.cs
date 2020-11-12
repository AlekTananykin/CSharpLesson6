using System;
using System.Runtime.Serialization;

namespace Task3
{
    [Serializable]
    internal class StudentsCountException : Exception
    {
        public StudentsCountException()
        {
        }

        public StudentsCountException(string message) : base(message)
        {
        }

        public StudentsCountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StudentsCountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}