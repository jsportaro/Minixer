using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.Messaging
{
    public class SimpleStream : ISimpleStream
    {

        public void Send<T>(T @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Action<T> eventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
