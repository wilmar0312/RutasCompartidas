using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var user = await _authService.LoginAsync(usuario.Email, usuario.Password);
            if (user == null) return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(new
            {
                user.Id,
                user.Nombre,
                user.Email,
                user.Rol
            });
        }
    }
}
