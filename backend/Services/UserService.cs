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
        //Declare private fields to hold the dbContext, mapper, and logger instances
        private readonly BackendDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<UserService> logger;

        //Define a constructor that takes in the dbContext, mapper, and logger instances as parameters
        public UserService(BackendDbContext dbContext, IMapper mapper, ILogger<UserService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        //Define the CreateUser method that takes in a UserModel instance and returns a User instance
        public async Task<User> CreateUser(UserModel model)
        {
            //Find the existing user with the same name as the one provided
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == model.Name)!;

            //If a user with the same name exists, throw an exception
            if (user != null)
                throw new ErrorResponseException(
                    StatusCodes.Status409Conflict,
                    $"User with name {model.Name} exist!",
                    new List<Error>()
                );

            //Create a new User instance and map the properties of the UserModel instance to it
            user = new User();
            mapper.Map<UserModel, User>(model, user);

            //Add the new User instance to the Users table in the database
            dbContext.Users.Add(user);

            //Save the changes to the database
            int result = await dbContext.SaveChangesAsync();

            //If the result of the save operation is less than 1, rollback the changes and throw an exception
            if (result < 1)
            {
                //Rollback the changes
                dbContext.Entry(user).Reload();
                throw new ErrorResponseException(
                    StatusCodes.Status500InternalServerError,
                    $"Failed saving user in db",
                    new List<Error>()
                );
            }

            //Log that the user was successfully created and return the user instance
            logger.LogInformation("User created!");
            return user;
        }

        //Define the GetUsers method that returns a collection of User instances
        public IEnumerable<User> GetUsers()
        {
            //Return all the User instances in the Users table in the database
            return dbContext.Users;
        }

        //Define the GetUser method that takes in an id parameter and returns a User instance
        public async Task<User> GetUser(int id)
        {
            //Find the user with the provided id in the Users table in the database
            User? user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            //If no user with the provided id exists, throw an exception
            if (user == null)
                throw new ErrorResponseException(
                    StatusCodes.Status404NotFound,
                    $"User with id {id} not found!",
                    new List<Error>()
                );

            //Return the user instance
            return user;
        }
    }
}