using backend.DTOs;

namespace backend.Interfaces
{
    public interface IUser
    {
        public UserModel CreateUser(UserModel model);
    }
}