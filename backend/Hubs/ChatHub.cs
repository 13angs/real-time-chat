
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs {
    public class ChatHub : Hub
    {
        private readonly BackendDbContext dbContext;

        public ChatHub(BackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
            Message newMessage = new Message{
                From=user,
                To="server",
                Text=message
            };

            dbContext.Messages.Add(newMessage);
            await dbContext.SaveChangesAsync();
            
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendNotification(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }

}
