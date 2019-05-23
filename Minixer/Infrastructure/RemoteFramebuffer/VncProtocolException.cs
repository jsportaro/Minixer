using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.RemoteFramebuffer
{
    public class VncProtocolException : ApplicationException
    {
        public VncProtocolException()
        {
        }

        public VncProtocolException(string message) : base(message)
        {
        }

        public VncProtocolException(string message, Exception inner) : base(message, inner)
        {
        }

        public VncProtocolException(SerializationInfo info, StreamingContext cxt) : base(info, cxt)
        {
        }
    }
}
