using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace MeuWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de modelo aqui, se necessário
        }
    }
}