namespace AplicacaoWebFilmes.Infra.Autenticação
{
    public class JwtConfiguracao
    {
        public string Senha { get; set; } = string.Empty;
        public int ExpiracaoEmMinutos { get; set; }
        public string Emissor { get; set; } = string.Empty;
        public string Publico { get; set; } = string.Empty;
    }
}
