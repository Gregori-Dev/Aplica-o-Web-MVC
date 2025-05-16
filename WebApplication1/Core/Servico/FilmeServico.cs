using Microsoft.EntityFrameworkCore;

public class FilmeServico
{
    private readonly AplicacaoDbContext _contexto;
    private readonly TMDBClientServico _tmdbClient;

    public FilmeServico(AplicacaoDbContext contexto,
                        TMDBClientServico tmdbClient)
    {
        _contexto = contexto;
        _tmdbClient = tmdbClient;
    }

    public async Task SalvarFilme(Filme filme)
    {
        try
        {
            var filmeEntidade = new FilmeEntidade
            {
                Id = filme.Id,
                Titulo = filme.Title,
                DataLancamento = filme.ReleaseDate,
                Sinopse = filme.Overview,
                MediaVotos = filme.VoteAverage,
                Generos = filme.Genres?.Select(g => new GeneroEntidade { Id = g.Id, Nome = g.Name }).ToList() ?? new List<GeneroEntidade>()
            };

            bool jaExiste = await _contexto.Filmes.AnyAsync(f => f.Id == filmeEntidade.Id);

            SalvarFilmesSomenteSe(filmeEntidade, jaExiste);
            await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            var innerException = dbEx.InnerException;
            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }
            Console.WriteLine($"Erro ao salvar o filme: {innerException.Message}");
            throw;
        }
    }

    private void SalvarFilmesSomenteSe(FilmeEntidade filmeEntidade, bool jaExiste)
    {
        if (jaExiste)        
            _contexto.Filmes.Update(filmeEntidade);
        
        else        
            _contexto.Filmes.Add(filmeEntidade);       
    }

    public async Task<List<FilmeEntidade>> ObterFilmesSalvos()
    {
        return await _contexto.Filmes.Include(f => f.Generos).ToListAsync();
    }

    public async Task<string> ObterPosterDoFilmeFavorito(int filmeId)
    {
        var filme = await _tmdbClient.ObterFilme(filmeId);
        return filme.IsSuccess ? filme.Filme.PosterPath : string.Empty;
    }
}
