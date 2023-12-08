using BibliotecaDB.Helper;
using BibliotecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AutoreController : Controller
    {
        private BibliotecaDbContext db = new();
        public IActionResult Create()
        {
            ViewBag.IdLibro = new SelectList(db.Libros, "IdLibro");
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Autore autore)
        {
            ViewBag.IdLibro = new SelectList(db.Libros, "IdLibro");
            ViewBag.IdAutor = new SelectList(db.Autores, "IdAutor");
            if (ModelState.IsValid)
            {
                var p = db.Autores.Find(autore.IdAutor);
                if (p != null)
                {
                    ModelState.AddModelError("IdAutor", "ya esta registrado");
                    return View();
                }
                db.Autores.Add(autore);
                db.SaveChanges();
            }
            return View();
        }
        public async Task<IActionResult> Index(string buscar, string filtro, int? numPag)
        {
            if (buscar == null)
                buscar = filtro;
            else
                numPag = 1;
            ViewData["filtro"] = buscar;
            var cli = from c in db.Autores select c;
            if (!string.IsNullOrEmpty(buscar))
            {
                cli = cli.Where(x => x.NombreAutor.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2;
            return View(await PaginatedList<Autore>.CreateAsync(cli, numPag ?? 1, tamPag));
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                var cat = db.Autores.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Autore autore)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Autores.FirstOrDefault(
                    x => x.IdAutor != autore.IdAutor);
                if (cat != null)
                {
                    ModelState.AddModelError("NombreAutor", "La categoría esta registrada");
                }
                else
                {
                    db.Autores.Update(autore);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(autore);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Autores.Find(id);
            if (cat != null)
            {

                db.Autores.Remove(cat);
                db.SaveChanges();

                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}
