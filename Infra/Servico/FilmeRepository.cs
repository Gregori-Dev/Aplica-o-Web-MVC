using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

public class FilmeRepository
{
    private readonly AplicacaoDbContext _contexto;
    private readonly TMDBClientServico _tmdbClient;

    public FilmeRepository(AplicacaoDbContext contexto,
                        TMDBClientServico tmdbClient)
    {
        _contexto = contexto;
        _tmdbClient = tmdbClient;
    }

    public async Task SalvarFilme(Filme filme)
    {
        try
        {
            var filmeEntidade = new Filme
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                DataLancamento = filme.DataLancamento,
                Sinopse = filme.Sinopse,
                MediaVotos = filme.MediaVotos,
                Genero = filme.Genero?.Select(g => new Genero { Id = g.Id, Nome = g.Nome }).ToList() ?? new List<Genero>()
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

    private void SalvarFilmesSomenteSe(Filme filmeEntidade, bool jaExiste)
    {
        if (jaExiste)        
            _contexto.Filmes.Update(filmeEntidade);
        
        else        
            _contexto.Filmes.Add(filmeEntidade);       
    }

    public async Task<List<Filme>> ObterFilmesSalvos()
    {
        return await _contexto.Filmes.Include(f => f.Genero).ToListAsync();
    }

    public async Task<string> ObterPosterDoFilmeFavorito(int filmeId)
    {
        var filme = await _tmdbClient.ObterFilme(filmeId);
        return filme.IsSuccess ? filme.Filme.CaminhoPoster : string.Empty;
    }
}
