using backend.DTOs;

namespace backend.Interfaces
{
    public interface ILogin
    {
        public Task<LoginModel> Login(int userId);
    }
}