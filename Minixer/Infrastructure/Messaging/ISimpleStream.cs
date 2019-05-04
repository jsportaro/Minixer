using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.Messaging
{
    internal interface ISimpleStream
    {
        void Send<T>(T @event);

        void Subscribe<T>(Action<T> eventHandler);
    }
}
