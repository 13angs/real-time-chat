using backend.DTOs;
using backend.Models;

namespace backend.Interfaces
{
    public interface IUser
    {
        public Task<User> CreateUser(UserModel model);
        public IEnumerable<User> GetUsers();
    }
}