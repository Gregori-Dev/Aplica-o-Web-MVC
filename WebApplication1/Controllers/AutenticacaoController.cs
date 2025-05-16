using Microsoft.AspNetCore.Mvc;
using WebApplication1.Core.Entidades;
using WebApplication1.Core.Intefaces;

namespace WebApplication.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IServicoAutenticacao _servico;

        public AutenticacaoController(IServicoAutenticacao servico)
        {
            _servico = servico;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            // Aqui seria onde você valida o usuário. Exemplo simples:
            if (usuario.NomeUsuario == "admin" && usuario.Senha == "123")
            {
                var token = _servico.GerarToken(usuario);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}