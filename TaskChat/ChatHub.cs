using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TaskChat.Common;

namespace TaskChat
{
    //SignalR hub class for receiving and broadcasting messages and user information
    public class ChatHub : Hub
    {
        
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });

                //Initialize data when client connects
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                //Update chat user list on all clients
                Clients.AllExcept(id).onNewUserConnected(id, userName);

            }

        }

        public void SendMessageToAll(string userName, string message)
        {
            //Store previous 100 messages
            AddMessageinCache(userName, message);

            //Send message to all connected users
            Clients.All.messageReceived(userName, message);
        }

        public override Task OnDisconnected()
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected();
        }

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }
    }

}