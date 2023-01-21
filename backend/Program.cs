// configure the SignalR in the startup class

using backend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();

// using the UseEndpoints extension method to map the ChatHub to the /chat 
app.UseEndpoints(endpoints => {
    endpoints.MapHub<ChatHub>("/chat");
});

app.MapGet("/", () => "Hello World!");


app.Run();
