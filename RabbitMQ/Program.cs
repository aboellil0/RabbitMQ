using RabbitMQ.Services;
using RabbitMQ.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<RabbitMQContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
// Or for SQLite:
// options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add other services
builder.Services.AddOpenApi();
builder.Services.AddScoped<IMessageProduser, MessageProduser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();