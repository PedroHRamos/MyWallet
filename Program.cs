using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWallet.Features.Usuarios;
using MyWallet.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Monta a connection string dinamicamente
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyWalletDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Wait for SQL Server to be ready
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<MyWalletDbContext>();
        var maxAttempts = 30; // 30 seconds timeout
        var attempts = 0;
        
        while (attempts < maxAttempts)
        {
            try
            {
                db.Database.CanConnect();
                break;
            }
            catch
            {
                attempts++;
                Thread.Sleep(1000); // Wait 1 second before next attempt
            }
        }
        
        if (attempts == maxAttempts)
        {
            throw new Exception("Could not connect to SQL Server after 30 attempts");
        }

        try
        {
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
        
        
    }
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
