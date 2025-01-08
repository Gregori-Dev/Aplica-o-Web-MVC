using Microsoft.EntityFrameworkCore;

public class AplicacaoDbContext : DbContext
{
    public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options)
        : base(options)
    {
    }

    public DbSet<FilmeEntidade> Filmes { get; set; }
    public DbSet<GeneroEntidade> Generos { get; set; }
}
