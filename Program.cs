using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWallet.Features.Usuarios;
using MyWallet.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var masterConnectionString = connectionString.Replace("Database=MyWalletDb", "Database=master");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyWalletDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var masterContext = new MyWalletDbContext(
        new DbContextOptionsBuilder<MyWalletDbContext>()
            .UseSqlServer(masterConnectionString)
            .Options);

    var maxAttempts = 30;
    var attempts = 0;
    while (attempts < maxAttempts)
    {
        try
        {
            if (masterContext.Database.CanConnect()) break;
        }
        catch
        {
            attempts++;
            Thread.Sleep(1000);
        }
    }
    if (attempts == maxAttempts)
    {
        throw new Exception("Could not connect to SQL Server after 30 attempts");
    }

    masterContext.Database.ExecuteSqlRaw("IF DB_ID('MyWalletDb') IS NULL CREATE DATABASE [MyWalletDb];");
}

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<MyWalletDbContext>();
db.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();

