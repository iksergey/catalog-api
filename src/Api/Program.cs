var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddMarten(option =>
{
    option.Connection(connectionString);
}).UseLightweightSessions().InitializeWith<InitializeBookDatabase>();

var app = builder.Build();

app.Run();
