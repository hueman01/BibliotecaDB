using BibliotecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BibliotecaDbContext db = new();
        public HomeController(ILogger<HomeController> logger)
             
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
			
			return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {

			
			if (email == null || password == null)
            {
                ViewBag.Error = "Debe ingresar Email y Password";
                return View();
            }
			var user = db.Usuarios.FirstOrDefault(u => u.Usuario1 == email && u.Contrasena == password);
			if (user == null)
            {
                ViewBag.Error = "Email y Contraseña Incorecta";
                return View();
            }
			HttpContext.Session.SetString("usuario1", user.Usuario1);
			TempData["usuario1"] = HttpContext.Session.GetString("usuario1");
			return RedirectToAction("Index");
			
        }
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			TempData.Clear();
			return RedirectToAction("Login");
		}



		public IActionResult Index()
        {
			
			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		
		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}