using AutoMapper;
using backend.DTOs;
using backend.Exceptions;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUser
    {
        private readonly BackendDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<UserService> logger;

        public UserService(BackendDbContext dbContext, IMapper mapper, ILogger<UserService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<User> CreateUser(UserModel model)
        {
            // find the existing user
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == model.Name)!;

            if (user != null)
                throw new ErrorResponseException(
                    StatusCodes.Status409Conflict,
                    $"User with name {model.Name} exist!",
                    new List<Error>()
                );
            user = new User();
            mapper.Map<UserModel, User>(model, user);

            dbContext.Users.Add(user);
            int result = await dbContext.SaveChangesAsync();

            if (result < 1)
            {
                // rollback the changes
                dbContext.Entry(user).Reload();
                throw new ErrorResponseException(
                    StatusCodes.Status500InternalServerError,
                    $"Failed saving user in db",
                    new List<Error>()
                );
            }

            logger.LogInformation("User created!");
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users;
        }

        public async Task<User> GetUser(int id)
        {

            User? user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new ErrorResponseException(
                    StatusCodes.Status404NotFound,
                    $"User with id {id} not found!",
                    new List<Error>()
                );

            return user;
        }
    }
}