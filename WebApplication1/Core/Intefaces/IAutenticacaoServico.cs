using WebApplication1.Core.Entidades;

namespace WebApplication1.Core.Intefaces
{
    public interface IAutenticacaoServico
    {
        string GerarToken(Usuario usuario);
    }
}
