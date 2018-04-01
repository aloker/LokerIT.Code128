using System;
using System.Runtime.Serialization;

namespace LokerIT.Code128.Interpreter
{
    public class CodeError : Exception
    {
        public CodeError()
        {
        }

        protected CodeError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CodeError(string message) : base(message)
        {
        }

        public CodeError(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}