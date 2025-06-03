using Application.Intefaces;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoWebFilmes.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoServico _servico;

        public AutenticacaoController(IAutenticacaoServico servico)
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