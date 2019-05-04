using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Hubs
{
    public class RepositoryHub : Hub
    {


        public async Task SendMessage(string eventType)
        {
            await Clients.All.SendAsync("ReceiveMessage", eventType);
        }
    }
}
