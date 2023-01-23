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
        public IEnumerable<Message> GetMessages()
        {
            return dbContext.Messages;
        }
    }
}