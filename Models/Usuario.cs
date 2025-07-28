using System;
using Microsoft.EntityFrameworkCore;

namespace MyWallet.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string InvestorProfile { get; set; }
        public DateTime BirthDate { get; set; }
        public int Plan { get; set; }
    }

    public class MyWalletDbContext : DbContext
    {
        public MyWalletDbContext(DbContextOptions<MyWalletDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
