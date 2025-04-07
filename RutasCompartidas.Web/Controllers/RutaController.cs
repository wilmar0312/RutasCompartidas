using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;
using RutasCompartidas.Infrastructure.Persistence; // Para AppDbContext

namespace RutasCompartidas.Web.Controllers
{
    public class RutaController : Controller
    {
        private readonly RutaService _rutaService;
        private readonly AppDbContext _context;

        public RutaController(RutaService rutaService, AppDbContext context)
        {
            _rutaService = rutaService;
            _context = context;
        }

        // Mostrar todas las rutas
        public async Task<IActionResult> Index()
        {
            var rutas = await _rutaService.ObtenerRutasAsync();
            return View(rutas);
        }

        // Vista para crear una nueva ruta (Solo conductores)
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Ruta ruta)
        {
            ruta.ConductorId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

            if (ruta.ConductorId == 0)
                return RedirectToAction("Login", "Auth");

            await _rutaService.AgregarRutaAsync(ruta);
            return RedirectToAction("Index");
        }

        // Vista para editar una ruta
        public async Task<IActionResult> Editar(int id)
        {
            var ruta = await _rutaService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            return View(ruta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Ruta ruta)
        {
            await _rutaService.ActualizarRutaAsync(ruta);
            return RedirectToAction("Index");
        }

        // Eliminar una ruta
        public async Task<IActionResult> Eliminar(int id)
        {
            await _rutaService.EliminarRutaAsync(id);
            return RedirectToAction("Index");
        }

        // ----------------------
        // FUNCIONES DE UNIR / SALIR
        // ----------------------

        [HttpPost]
        public IActionResult Unirse(int id)
        {
            var ruta = _context.Rutas.Find(id);
            if (ruta == null) return NotFound();

            var yaUnido = HttpContext.Session.GetString($"Unido_{id}") == "true";
            if (!yaUnido)
            {
                ruta.CantidadPasajeros++;
                _context.SaveChanges();
                HttpContext.Session.SetString($"Unido_{id}", "true");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salir(int id)
        {
            var ruta = _context.Rutas.Find(id);
            if (ruta == null) return NotFound();

            var yaUnido = HttpContext.Session.GetString($"Unido_{id}") == "true";
            if (yaUnido)
            {
                ruta.CantidadPasajeros = Math.Max(0, ruta.CantidadPasajeros - 1);
                _context.SaveChanges();
                HttpContext.Session.SetString($"Unido_{id}", "false");
            }

            return RedirectToAction("Index");
        }
    }
}
