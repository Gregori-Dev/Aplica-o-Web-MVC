using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface ITMDBService
    {
        Task<Filme> ObterFilme(int id);
    }
}
