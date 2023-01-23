using AutoMapper;
using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
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
            try{
                // find the existing user
                User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == model.Name)!;

                // if user exist
                // throw the error
                if(user != null)
                    throw new Exception($"User with name {model.Name} exist!");

                // if not exist
                // create a new user
                user = new User();
                mapper.Map<UserModel, User>(model, user);

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("User created!");
                return user;

            }catch (Exception e){
                logger.LogError(e.Message);
                throw new Exception();
            }
        }
    
        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users;
        }
    }
}