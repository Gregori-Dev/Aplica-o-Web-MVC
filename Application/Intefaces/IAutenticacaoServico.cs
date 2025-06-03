using Domain.Entidades;

namespace Application.Intefaces
{
    public interface IAutenticacaoServico
    {
        string GerarToken(Usuario usuario);
    }
}
