using Microsoft.EntityFrameworkCore;
using MyWalletWebAPI.Domain;

namespace MyWalletWebAPI.Models.Database;

public class MyWalletDbContext : DbContext
{
    public MyWalletDbContext(DbContextOptions<MyWalletDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<AssetCategory> AssetCategories { get; set; }
}