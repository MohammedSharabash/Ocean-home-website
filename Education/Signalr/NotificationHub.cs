using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Signalr
{
    public class NotificationHub:Hub
    {
        public async Task notify(Notification notification)
        {
            await Clients.All.SendAsync("reciveNotify", notification.Body);

        }
    }
}
