using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

public class AplicacaoDbContext : DbContext
{
    public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Genero> Generos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>()
            .HasMany(f => f.Genero)
            .WithOne()
            .HasForeignKey("FilmeId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Genero>()
            .HasKey(g => g.Id);
    }
}
