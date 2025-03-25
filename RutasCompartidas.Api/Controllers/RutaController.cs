using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Api.Controllers
{
    [Route("api/rutas")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly RutaService _rutaService;

        public RutaController(RutaService rutaService)
        {
            _rutaService = rutaService;
        }

        // 📌 Obtener todas las rutas
        [HttpGet]
        public async Task<IActionResult> GetRutas()
        {
            var rutas = await _rutaService.ObtenerRutasAsync();
            return Ok(rutas);
        }

        // 📌 Obtener una ruta por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRuta(int id)
        {
            var ruta = await _rutaService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            return Ok(ruta);
        }

        // 📌 Crear una nueva ruta (Solo conductores)
        [HttpPost]
        public async Task<IActionResult> CrearRuta([FromBody] Ruta ruta)
        {
            if (ruta.ConductorId == 0)
                return BadRequest("El ConductorId es obligatorio.");

            await _rutaService.AgregarRutaAsync(ruta);
            return CreatedAtAction(nameof(GetRuta), new { id = ruta.Id }, ruta);
        }

        // 📌 Editar una ruta (Solo conductores)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarRuta(int id, [FromBody] Ruta ruta)
        {
            var rutaExistente = await _rutaService.ObtenerRutaPorIdAsync(id);
            if (rutaExistente == null) return NotFound();

            ruta.ConductorId = rutaExistente.ConductorId; // Mantener el conductor original

            await _rutaService.ActualizarRutaAsync(ruta);
            return NoContent();
        }

        // 📌 Eliminar una ruta (Solo conductores)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRuta(int id)
        {
            await _rutaService.EliminarRutaAsync(id);
            return NoContent();
        }
    }
}
