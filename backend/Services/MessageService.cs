using backend.Exceptions;
using backend.Interfaces;
using backend.Models;

namespace backend.Services
{
    public class MessageService : IMessage
    {
        private readonly BackendDbContext dbContext;

        public MessageService(BackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Message> GetMessages(int from, int to)
        {
            // try{
            // get the user by from 
            User? user = dbContext.Users
                .FirstOrDefault(u => u.Id == from);

            if (user == null)
                throw new ErrorResponseException(
                    StatusCodes.Status404NotFound,
                    "User doesn't exist!",
                    new List<Error>()
                );

            // get the message by user.id and to
            IEnumerable<Message> messages = dbContext.Messages
                .Where(m => (m.From == user.Id && m.To == to) ||
                            m.From == to && m.To == user.Id);

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