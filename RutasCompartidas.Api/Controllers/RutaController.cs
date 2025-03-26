using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Domain.Entities;
using RutasCompartidas.Infrastructure.Persistence;

namespace RutasCompartidas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RutaController : ControllerBase
{
    private readonly Infrastructure.Persistence.RutasCompartidasContext _context;

    public RutaController(Infrastructure.Persistence.RutasCompartidasContext context)
    {
        _context = context;
    }

    // 📌 Obtener todas las rutas
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(string filter = "")
    {
        var rutas = await _context.Rutas.ToListAsync();

        var list = rutas.Select(ruta => new RutaDto
        {
            Id = ruta.Id,
            Origen = ruta.Origen,
            Destino = ruta.Destino,
            FechaHora = ruta.FechaHora,
            ConductorId = ruta.ConductorId
        }).ToList();

        return Ok(list);
    }

    // 📌 Obtener una ruta por ID
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var ruta = await _context.Rutas.FindAsync(id);
        if (ruta == null)
        {
            return BadRequest("Ruta no encontrada");
        }

        var dto = new RutaDto
        {
            Id = ruta.Id,
            Origen = ruta.Origen,
            Destino = ruta.Destino,
            FechaHora = ruta.FechaHora,
            ConductorId = ruta.ConductorId
        };

        return Ok(dto);
    }

    // 📌 Crear una nueva ruta (Solo conductores)
    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] RutaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos inválidos");
        }

        var ruta = new Ruta
        {
            Origen = dto.Origen,
            Destino = dto.Destino,
            FechaHora = dto.FechaHora,
            ConductorId = dto.ConductorId
        };

        _context.Rutas.Add(ruta);
        await _context.SaveChangesAsync();
        return Ok(new { success = true, message = "Ruta creada con éxito!" });
    }

    // 📌 Editar una ruta (Solo conductores)
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] RutaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos inválidos");
        }

        var ruta = await _context.Rutas.FindAsync(dto.Id);
        if (ruta == null)
        {
            return BadRequest("Ruta no encontrada");
        }

        ruta.Origen = dto.Origen;
        ruta.Destino = dto.Destino;
        ruta.FechaHora = dto.FechaHora;

        try
        {
            _context.Update(ruta);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest("Error al actualizar la ruta");
        }

        return Ok(new { success = true, message = "Ruta actualizada con éxito!" });
    }

    // 📌 Eliminar una ruta (Solo conductores)
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ruta = await _context.Rutas.FindAsync(id);
        if (ruta == null)
        {
            return BadRequest("Ruta no encontrada");
        }

        _context.Rutas.Remove(ruta);
        await _context.SaveChangesAsync();
        return Ok(new { success = true, message = "Ruta eliminada con éxito!" });
    }
}
