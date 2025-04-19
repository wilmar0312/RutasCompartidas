using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Domain.Entities;
using RutasCompartidas.Web.Services;

namespace RutasCompartidas.Web.Controllers
{
    public class RutaController : Controller
    {
        private readonly RutaApiService _rutaApiService;

        public RutaController(RutaApiService rutaApiService)
        {
            _rutaApiService = rutaApiService;
        }

        public async Task<IActionResult> Index()
        {
            var rutas = await _rutaApiService.ObtenerRutasAsync();
            return View(rutas);
        }

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

            await _rutaApiService.CrearRutaAsync(ruta);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var ruta = await _rutaApiService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            return View(ruta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Ruta ruta)
        {
            await _rutaApiService.ActualizarRutaAsync(ruta);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            await _rutaApiService.EliminarRutaAsync(id);
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Unirse(int id)
        {
            var ruta = await _rutaApiService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            var yaUnido = HttpContext.Session.GetString($"Unido_{id}") == "true";
            if (!yaUnido)
            {
                ruta.CantidadPasajeros++;
                await _rutaApiService.ActualizarRutaAsync(ruta);
                HttpContext.Session.SetString($"Unido_{id}", "true");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Salir(int id)
        {
            var ruta = await _rutaApiService.ObtenerRutaPorIdAsync(id);
            if (ruta == null) return NotFound();

            var yaUnido = HttpContext.Session.GetString($"Unido_{id}") == "true";
            if (yaUnido)
            {
                ruta.CantidadPasajeros = Math.Max(0, ruta.CantidadPasajeros - 1);
                await _rutaApiService.ActualizarRutaAsync(ruta);
                HttpContext.Session.SetString($"Unido_{id}", "false");
            }

            return RedirectToAction("Index");
        }
    }
}

