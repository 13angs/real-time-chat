using backend.Models;

namespace backend
{
    public static class Routes
    {
        public static void Message(WebApplication app)
        {
            app.MapGet("/api/v1", () => "Chat is running!");
            app.MapGet("/api/v1/messages", (BackendDbContext dbContext) =>
            {
                return dbContext.Messages;
            });
        }
    }
}