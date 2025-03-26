using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Web.Controllers
{
    public class RutaController : Controller
    {
        private readonly RutaService _rutaService;

        public RutaController(RutaService rutaService)
        {
            _rutaService = rutaService;
        }

        // Mostrar todas las rutas (para pasajero y conductor)
        public async Task<IActionResult> Index()
        {
            var rutas = await _rutaService.ObtenerRutasAsync();
            return View(rutas);
        }

        //Vista para crear una nueva ruta (Solo conductores)
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

        // Vista para editar una ruta (Solo conductores)
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

        // Eliminar una ruta (Solo conductores)
        public async Task<IActionResult> Eliminar(int id)
        {
            await _rutaService.EliminarRutaAsync(id);
            return RedirectToAction("Index");
        }
    }
}
