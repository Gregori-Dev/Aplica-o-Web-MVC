using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IFilmeRepository
    {
        Task<bool> JaExisteAsync(int id);
        Task SalvarAsync(Filme filme);
        Task<List<Filme>> ObterTodosAsync();
    }

}
