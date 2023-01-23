using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class LoginService : ILogin
    {
        private readonly BackendDbContext context;

        public LoginService(BackendDbContext context)
        {
            this.context = context;
        }
        public async Task<LoginModel> Login(int userId)
        {
            try {
                User? user = await context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);
                
                if(user != null)
                    return new LoginModel{
                        Id=user.Id,
                        Message="Successfully login!"
                    };
                throw new Exception("Login failed");
            }catch (Exception e){
                throw new Exception(e.Message);
            }
        }
    }
}