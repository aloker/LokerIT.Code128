using System;
using System.Runtime.Serialization;

namespace LokerIT.Code128.Interpreter
{
    public class EndOfInputException : Exception
    {
        public EndOfInputException()
        {
        }

        protected EndOfInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EndOfInputException(string message) : base(message)
        {
        }

        public EndOfInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}