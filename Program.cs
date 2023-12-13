using Microsoft.EntityFrameworkCore;
using todo_csharp.Data; // Replace with your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); // This line adds the controllers to the services

var app = builder.Build();

app.MapControllers(); // This line maps controller routes

app.Run();
