using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly RutaService _rutaService;

        public RutasController(RutaService rutaService)
        {
            _rutaService = rutaService;
        }

        // GET: api/rutas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rutas = await _rutaService.ObtenerRutasAsync();
            return Ok(rutas);
        }

        // GET: api/rutas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ruta = await _rutaService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            return Ok(ruta);
        }

        // POST: api/rutas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ruta ruta)
        {
            await _rutaService.AgregarRutaAsync(ruta);
            return CreatedAtAction(nameof(GetById), new { id = ruta.Id }, ruta);
        }

        // PUT: api/rutas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ruta ruta)
        {
            if (id != ruta.Id)
                return BadRequest("ID de la ruta no coincide");

            await _rutaService.ActualizarRutaAsync(ruta);
            return NoContent();
        }

        // DELETE: api/rutas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rutaService.EliminarRutaAsync(id);
            return NoContent();
        }
    }
}
