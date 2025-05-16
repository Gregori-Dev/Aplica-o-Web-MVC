using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Core.Entidades;
using WebApplication1.Core.Intefaces;
using WebApplication1.Infra.Autenticação;

public class AutenticacaoServico : IServicoAutenticacao
{
    private readonly JwtConfiguracao _jwtConfiguracao;

    public AutenticacaoServico(IOptions<JwtConfiguracao> configuracaoJwt)
    {
        _jwtConfiguracao = configuracaoJwt.Value;
    }

    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NomeUsuario),
            new Claim(ClaimTypes.Role, usuario.Perfil),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguracao.Senha));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtConfiguracao.Emissor,
            audience: _jwtConfiguracao.Publico,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfiguracao.ExpiracaoEmMinutos),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
