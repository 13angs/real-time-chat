using backend.Models;

namespace backend.Interfaces
{
    public interface IMessage
    {
        public IEnumerable<Message> GetMessages(int from, int to);
    }
}