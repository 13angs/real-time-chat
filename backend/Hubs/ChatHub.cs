
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
        public async Task SendMessage(string from, string to)
        {
            // Send the "ReceiveMessage" signal to all clients with the "from" and "message" parameters
            await Clients.All.SendAsync("ReceiveMessage", from, to);
        }

        // This method sends a notification to a user
        public async Task SendNotification(string user, string message)
        {
            // Send the "ReceiveNotification" signal to the specified user with the "message" parameter
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }

}
