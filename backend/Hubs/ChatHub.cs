
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly BackendDbContext dbContext;

        // Constructor for the ChatHub class which accepts an instance of BackendDbContext
        public ChatHub(BackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // This method sends a message to a user
        public async Task SendMessage(string from, string to, string message)
        {
            // Convert the "from" and "to" strings to integers
            int fromInt;
            int toInt;
            Int32.TryParse(from, out fromInt);
            Int32.TryParse(to, out toInt);

            // Find the user associated with the "from" parameter
            User? user = dbContext.Users
                .FirstOrDefault(u => u.Id == fromInt);

            // Create a new message object with the specified data
            Message newMessage = new Message
            {
                From = fromInt,
                To = toInt,
                Text = message,
                UserId = user!.Id,
                User = user
            };

            // Add the new message to the database and save changes
            dbContext.Messages.Add(newMessage);
            await dbContext.SaveChangesAsync();

            // Send the "ReceiveMessage" signal to all clients with the "from" and "message" parameters
            await Clients.All.SendAsync("ReceiveMessage", from, message);
        }

        // This method sends a notification to a user
        public async Task SendNotification(string user, string message)
        {
            // Send the "ReceiveNotification" signal to the specified user with the "message" parameter
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }

}
