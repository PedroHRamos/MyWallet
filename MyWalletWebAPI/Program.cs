using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MyWallet.Services.Users;
using MyWalletWebAPI.Models.Database;
using MyWalletWebAPI.Repositories.Users;
using MyWalletWebAPI.Services.Users;

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

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Criar escopo para obter contexto do banco MyWalletDb
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<MyWalletDbContext>();

// Aplica as migra��es para criar/atualizar esquema, assume que o banco j� existe
db.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();