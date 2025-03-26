using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Application.Services;

namespace RutasCompartidas.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

      [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _authService.LoginAsync(email, password);
        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Rol);

            //  Si el usuario es Conductor, lo redirige a la página de gestión de rutas
            if (user.Rol == "Conductor")
            {
                return RedirectToAction("Index", "Ruta");
            }

            //  Si el usuario es Pasajero, lo redirige a ver las rutas disponibles
            return RedirectToAction("Index", "Ruta");
        }

        ViewBag.Error = "Credenciales incorrectas";
        return View();
    }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
