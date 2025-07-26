using Microsoft.EntityFrameworkCore;
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

// Executa as migrations automaticamente no startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyWalletDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
