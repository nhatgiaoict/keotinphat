using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LGCoreApp.Application.ViewModels.System;

namespace LGCoreApp.SignalR
{
    public class TeduHub : Hub
    {
        public async Task SendMessage(AnnouncementViewModel message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
