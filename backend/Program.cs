// configure the SignalR in the startup class
using backend;
using backend.Exceptions;
using backend.Hubs;
using backend.Interfaces;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: configuration["CorsName"]!, build =>
    {
        build.WithOrigins(configuration["AllowedHosts"]!)
    .AllowAnyHeader()
    .AllowAnyMethod();

    });
});

// Set up the environment
var env = builder.Environment;

// Set up the database context
builder.Services.AddDbContext<BackendDbContext>(options =>
{
string DbPath = System.IO.Path.Join(env.ContentRootPath, "data", "chat.db");
options.UseSqlite($"Data Source={DbPath}");
});

// Add Controllers and configure JSON serializer
builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
= new DefaultContractResolver()
);

// Add SignalR and AutoMapper services
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMessage, MessageService>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IUser, UserService>();

// Build the application
var app = builder.Build();

// Set up routing
app.UseRouting();

// Map the routes for Messages, Users, and Logins
Routes.Message(app);
Routes.User(app);
Routes.Login(app);

// Map the ChatHub for SignalR
app.MapHub<ChatHub>("/hub/chat");

// Use the CORS policy
app.UseCors(configuration["CorsName"]!);

// Use the response exception handler middleware
app.UseResponseExceptionHandler();

// Migrate the database on startup
using(var scope = app.Services.CreateScope())
{
BackendDbContext dbContext = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
dbContext.Database.Migrate();
}

// Run the application
app.Run();