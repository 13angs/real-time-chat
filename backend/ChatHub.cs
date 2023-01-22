
using Microsoft.AspNetCore.SignalR;

namespace backend {
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendNotification(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }

}
