// configure the SignalR in the startup class
using backend;
using backend.Hubs;
using backend.Interfaces;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

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

// configure controller to use Newtonsoft as a default serializer
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver()
);

builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMessage, MessageService>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IUser, UserService>();

var app = builder.Build();

app.UseRouting();

// all the routes
Routes.Message(app);
Routes.User(app);
Routes.Login(app);

app.MapHub<ChatHub>("/hub/chat");

app.UseCors(configuration["CorsName"]!);


// migrate the database on startup
using(var scope = app.Services.CreateScope())
{
  BackendDbContext dbContext = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
  dbContext.Database.Migrate();
}
app.Run();
