using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MyWallet.Features.Usuarios;
using MyWallet.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext apontando direto para o banco MyWalletDb
builder.Services.AddDbContext<MyWalletDbContext>(options =>
    options.UseSqlServer(connectionString)
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Criar escopo para obter contexto do banco MyWalletDb
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<MyWalletDbContext>();

// Aplica as migrações para criar/atualizar esquema, assume que o banco já existe
db.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();