using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend
{
    public static class Routes
    {
        public static void Message(WebApplication app)
        {
            app.MapGet("/api/v1", () => "Chat is running!");
            app.MapGet("/api/v1/messages", (IMessage message) =>
            {
                return message.GetMessages();
            });
        }

        public static void User(WebApplication app)
        {
            app.MapGet("/api/v1/users", (IUser user) =>
            {
                return user.GetUsers();
            });

            app.MapPost("/api/v1/users", async (IUser user, [FromBody] UserModel model) =>
            {
                return await user.CreateUser(model);
            });
        }
        public static void Login(WebApplication app)
        {
            app.MapPost("/api/v1/login/{userId}", (ILogin login, [FromRoute] int userId) =>
            {
                return login.Login(userId);
            });

        }
    }
}