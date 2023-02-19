using backend.DTOs;
using backend.Models;

namespace backend.Interfaces
{
    public interface IMessage
    {
        public IEnumerable<Message> GetMessages(int from, int to);
        public Task CreateMessage(MessageModel model);
    }
}