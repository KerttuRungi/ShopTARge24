
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Shop.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Broadcasting the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}