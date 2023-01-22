// configure the SignalR in the startup class
using backend;
using backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// configure cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: configuration["CorsName"]!, build =>
    {
        build.WithOrigins(configuration["AllowedHosts"]!)
          .AllowAnyHeader()
          .AllowAnyMethod();

    });
});

var env = builder.Environment;

builder.Services.AddDbContext<BackendDbContext>(options =>
{
    string DbPath = System.IO.Path.Join(env.ContentRootPath, "data", "chat.db");
    options.UseSqlite($"Data Source={DbPath}");
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();

// using the UseEndpoints extension method to map the ChatHub to the /chat 
// app.UseEndpoints(endpoints => {
//     endpoints.MapHub<ChatHub>("/chat");
// });
app.MapGet("/api/v1", () => "Chat is running!");
app.MapGet("/api/v1/messages", (BackendDbContext dbContext)=> {
  return dbContext.Messages;
});

app.MapHub<ChatHub>("/hub/chat");

app.UseCors(configuration["CorsName"]!);


// migrate the database on startup
using(var scope = app.Services.CreateScope())
{
  BackendDbContext dbContext = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
  dbContext.Database.Migrate();
  // dbContext.Database.EnsureCreated();
}
app.Run();
