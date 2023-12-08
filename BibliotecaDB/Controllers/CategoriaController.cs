using Microsoft.AspNetCore.Mvc;

using BibliotecaDB.Models;

using BibliotecaDB.Helper;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class CategoriaController : Controller
    {
        private BibliotecaDbContext db = new();
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            //verifica si el modelo es coherente con la clase
            if (ModelState.IsValid)
            {
                //SELECT * FROM Categori WHERE nombre= 'nombre'
                var cat = db.Categorias.FirstOrDefault(
                    c => c.NombreCategoria == categoria.NombreCategoria);
                if (cat != null)
                {
                    ModelState.AddModelError("NombreCategoria", "La categoría ya está registrada");
                    return View();
                }
                //INSERT INTO Categoria(nombre) VALUES ('nombre categoría')
                db.Categorias.Add(categoria);
                //guardar en la db 
                db.SaveChanges();
                //RedirectToAction sirve para redireccionar a alguna otra vista 
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Index(string buscar, string filtro, int? numPag)
        {
            //verificar si trae filtro o no
            if (buscar == null)
                buscar = filtro;
            else
                numPag = 1;
            //lleva el texto buscado a la página
            ViewData["filtro"] = buscar;
            var categoria = from c in db.Categorias select c;
            //verifica si no está vacio 
            if (!string.IsNullOrEmpty(buscar))
            {
                categoria = categoria.Where(
                    x => x.NombreCategoria.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2; //poner 20 o 25
            return View(await PaginatedList<Categoria>
                .CreateAsync(categoria, numPag ?? 1, tamPag));
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                //Find sirve para buscar por id o clave primaria 
                var cat = db.Categorias.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Categorias.FirstOrDefault(
                    x => x.IdCategoria != categoria.IdCategoria &&
                    x.NombreCategoria == categoria.NombreCategoria);
                if (cat != null)
                {
                    ModelState.AddModelError("NombreCategoria", "La categoría esta registrada");
                }
                else
                {
                    db.Categorias.Update(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(categoria);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Categorias.Find(id);
            if (cat != null)
            {

                //verifica si el id está presente en producto
                var prod = db.Categorias.FirstOrDefault(x => x.IdCategoria == id);
                if (prod != null)
                {
                    return Json("No se puede eliminar porque tiene datos asosiados");
                }
                db.Categorias.Remove(cat);
                db.SaveChanges();
                //retorna un mensaje en formato json para ser capturado por javascript
                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}

