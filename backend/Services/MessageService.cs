using backend.Exceptions;
using backend.Interfaces;
using backend.Models;

namespace backend.Services
{
    public class MessageService : IMessage
    {
        private readonly BackendDbContext dbContext;

        //Define the MessageService class with a constructor that takes in a BackendDbContext instance as a parameter
        public MessageService(BackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Define the GetMessages method that takes in a from and to parameter and returns an IEnumerable<Message> instance
        public IEnumerable<Message> GetMessages(int from, int to)
        {
            //Find the user with the provided from id in the Users table in the database
            User? user = dbContext.Users
                .FirstOrDefault(u => u.Id == from);

            //If no user with the provided from id exists, throw an exception
            if (user == null)
                throw new ErrorResponseException(
                    StatusCodes.Status404NotFound,
                    "User doesn't exist!",
                    new List<Error>()
                );

            //Find all the messages in the Messages table in the database where the From id matches the user id and the To id matches the provided to id, or vice versa
            IEnumerable<Message> messages = dbContext.Messages
                .Where(m => (m.From == user.Id && m.To == to) ||
                            m.From == to && m.To == user.Id);

            //Map each message to a new Message instance with the user information included, and return the resulting IEnumerable<Message> instance
            messages = messages.Select(m => new Message
            {
                Id = m.Id,
                UserId = user.Id,
                From = m.From,
                To = m.To,
                Text = m.Text,
                CreatedDate = m.CreatedDate,
                ModifiedDate = m.ModifiedDate,
                User = user
            });
            return messages;
        }
    }
}