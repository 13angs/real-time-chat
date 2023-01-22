// configure the SignalR in the startup class

using backend;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// configure cors
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: configuration["CorsName"], build =>
  {
    build.WithOrigins(configuration["AllowedHosts"])
        .AllowAnyHeader()
        .AllowAnyMethod();

  });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();

// using the UseEndpoints extension method to map the ChatHub to the /chat 
// app.UseEndpoints(endpoints => {
//     endpoints.MapHub<ChatHub>("/chat");
// });
app.MapGet("/api/v1", () => "Chat is running!");
app.MapHub<ChatHub>("/chat");

app.UseCors(configuration["CorsName"]);
app.Run();
