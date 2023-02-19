using backend.DTOs;
using backend.Exceptions;
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

        //Define the Login method that takes in a userId parameter and returns a LoginModel instance
        public async Task<LoginModel> Login(int userId)
        {
            //Find the user with the provided id in the Users table in the database
            User? user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
                return new LoginModel
                {
                    Id = user.Id,
                    Message = "Successfully login!"
                };
            
            //If no user with the provided id exists, throw an exception
            throw new ErrorResponseException(
                StatusCodes.Status404NotFound,
                "Login failed",
                new List<Error>()
            );
        }
    }
}