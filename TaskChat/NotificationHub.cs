using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TaskChat
{
    //SignalR hub class for receiving and broadcasting card data
    public class NotificationHub : Hub
    {
        static string currentMessage = "";

        public void Connect()
        {
            Clients.Caller.receiveNotification(currentMessage);

        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendNotifications(string message)
        {
            currentMessage = message;
            Clients.All.receiveNotification(message);
        }
    }
}