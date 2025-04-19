using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario model)
        {
            var user = await _authService.LoginAsync(model.Email, model.Password);
            if (user == null) return Unauthorized("Credenciales inválidas");

            return Ok(user);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario usuario)
        {
            var existente = await _authService.ObtenerUsuarioPorEmailAsync(usuario.Email);
            if (existente != null)
                return Conflict("Este correo ya está registrado.");

            await _authService.RegistrarAsync(usuario);
            return Ok(usuario);
        }
    }
}
