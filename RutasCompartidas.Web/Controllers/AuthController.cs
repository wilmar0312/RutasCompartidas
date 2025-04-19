using Microsoft.AspNetCore.Mvc;
using RutasCompartidas.Web.Services;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;

        public AuthController(AuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _authApiService.LoginAsync(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserRole", user.Rol);
                HttpContext.Session.SetString("UserName", user.Nombre);

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

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            var error = await _authApiService.RegistroAsync(usuario);
            if (error != null)
            {
                ViewBag.Error = error;
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
