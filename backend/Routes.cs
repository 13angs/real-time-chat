using backend.Interfaces;

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
            app.MapPost("/api/v1/users", (IMessage message) =>
            {
                return message.GetMessages();
            });
        }
    }
}