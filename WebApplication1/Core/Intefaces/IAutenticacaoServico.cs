using AplicacaoWebFilmes.Core.Entidades;

namespace AplicacaoWebFilmes.Core.Intefaces
{
    public interface IAutenticacaoServico
    {
        string GerarToken(Usuario usuario);
    }
}
