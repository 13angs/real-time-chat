
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
        public async Task SendMessage(string from, string to, string message)
        {
            Console.WriteLine($"{from}: {message}");

            int fromInt;
            int toInt;
            Int32.TryParse(from, out fromInt);
            Int32.TryParse(to, out toInt);

            User? user = dbContext.Users
                .FirstOrDefault(u => u.Id == fromInt);

            Message newMessage = new Message{
                From=fromInt,
                To=toInt,
                Text=message,
                UserId=user!.Id,
                User=user
            };

            dbContext.Messages.Add(newMessage);
            await dbContext.SaveChangesAsync();
            
            await Clients.All.SendAsync("ReceiveMessage", from, message);
        }

        public async Task SendNotification(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }

}
