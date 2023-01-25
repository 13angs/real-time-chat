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
            try{
                // get the user by from 
                User? user = dbContext.Users
                    .FirstOrDefault(u => u.Id == from);
                
                if(user == null)
                    throw new Exception("User doesn't exist!");
                
                // get the message by user.id and to
                IEnumerable<Message> messages = dbContext.Messages
                    .Where(m => m.From == user.Id &&
                        m.To == to
                    );
                
                return messages;

            }catch (Exception e){
                throw new Exception(e.Message);
            }
        }
    }
}