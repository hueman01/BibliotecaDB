using BibliotecaDB.Helper;
using BibliotecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LibroController : Controller
    {
        
        private BibliotecaDbContext db = new();
     
        public IActionResult Create()
        { //permite enviar la lista de categorías y pasarla a un select en la vista
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "Nacionalidad");
            ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "NombreAutor");
            return View();

        }
        [HttpPost]
        public IActionResult Create(Libro libro)
        {
           
            if (ModelState.IsValid)
            {
                ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "Nacionalidad");
                ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
                ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "NombreAutor");
                var p = db.Libros.Find(libro.Titulo);
                if (p != null)
                {
                    ModelState.AddModelError("IdLibro", "ya esta registrado");
                    return View();
                }
                db.Libros.Add(libro);
                db.SaveChanges();
            }
            return View();
        }
        public async Task<IActionResult> Index(string buscar, string filtro, int? numPag)
        {
            var e=db.Libros.Include(p=>p.IdAutorNavigation).ToList();
            var p = db.Libros.Include(p => p.IdCategoriaNavigation).ToList();
            var a = db.Libros.Include(p => p.IdAutorNavigation).ToList();

            ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "NombreAutor");
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "Nacionalidad");

            if (buscar == null)
                buscar = filtro;
            else
                numPag = 1;
            ViewData["filtro"] = buscar;
            var libro = from c in db.Libros select c;
            if (!string.IsNullOrEmpty(buscar))
            {
                libro = libro.Where(x => x.Titulo.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2;
            return View(await PaginatedList<Libro>.CreateAsync(libro, numPag ?? 1, tamPag));
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "Titulo");
                ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
                ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor", "NombreAutor");
                var cat = db.Libros.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Libros.FirstOrDefault(
                    x => x.IdLibro != libro.IdLibro &&
                    x.Titulo == libro.Titulo &&
                    x.AnioPublicacion == libro.AnioPublicacion &&
                    x.CantidadDisponible == libro.CantidadDisponible);
                if (cat != null)
                {
                    ModelState.AddModelError("Titulo", "La categoría esta registrada");
                }
                else
                {
                    db.Libros.Update(libro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(libro);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Libros.Find(id);
            if (cat != null)
            {

                db.Libros.Remove(cat);
                db.SaveChanges();

                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}

