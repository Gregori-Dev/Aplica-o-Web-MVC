using Microsoft.EntityFrameworkCore;

public class AplicacaoDbContext : DbContext
{
    public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options)
        : base(options)
    {
    }

    public DbSet<FilmeEntidade> Filmes { get; set; }
    public DbSet<GeneroEntidade> Generos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FilmeEntidade>()
            .HasMany(f => f.Generos)
            .WithOne()
            .HasForeignKey("FilmeId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GeneroEntidade>()
            .HasKey(g => g.Id);
    }
}
