using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaDB.Models;
using BibliotecaDB.Helper;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PrestamoController : Controller
    {
        private BibliotecaDbContext db = new();
       
        public IActionResult Create()
        {
            
            //permite enviar la lista de categorías y pasarla a un select en la vista
            ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
            return View();

        }
        [HttpPost]
        public IActionResult Create(Prestamo prestamo)
        {
            
            //verifica si el modelo es coherente con la clase
            if (ModelState.IsValid)
            {
                ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
                ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
                //SELECT * FROM Categori WHERE nombre= 'nombre'
                var cat = db.Prestamos.FirstOrDefault(
                    c => c.IdPrestamo == prestamo.IdPrestamo);
                if (cat != null)
                {
                    ModelState.AddModelError("IdPrestamo", "La categoría ya está registrada");
                    return View();
                }
                //INSERT INTO Categoria(nombre) VALUES ('nombre categoría')
                db.Prestamos.Add(prestamo);
                //guardar en la db 
                db.SaveChanges();
                //RedirectToAction sirve para redireccionar a alguna otra vista 
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Index(string buscar, string filtro, int? numPag)
        {
            var p = db.Prestamos.Include(p => p.IdCategoriaNavigation).ToList();
            var a = db.Prestamos.Include(p => p.IdClienteNavigation).ToList();

            //verificar si trae filtro o no
            if (buscar == null)
                buscar = filtro;
            else
                numPag = 1;
            //lleva el texto buscado a la página
            ViewData["filtro"] = buscar;
            var prestamo = from c in db.Prestamos select c;
            
            //verifica si no está vacio 
            if (!string.IsNullOrEmpty(buscar))
            {
                prestamo = prestamo.Where(
                    x => x.Titulo.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2; //poner 20 o 25
            return View(await PaginatedList<Prestamo>
                .CreateAsync(prestamo, numPag ?? 1, tamPag));
        }
    
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                //permite enviar la lista de categorías y pasarla a un select en la vista
                ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NombreCategoria");
                ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
                

                //Find sirve para buscar por id o clave primaria 
                var cat = db.Prestamos.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Prestamo prestamo)
        {
            
            if (ModelState.IsValid)
            {           

                var cat = db.Prestamos.FirstOrDefault(
                    x => x.IdPrestamo != prestamo.IdPrestamo &&
                    x.Titulo == prestamo.Titulo);
                if (cat != null)
                {
                    ModelState.AddModelError("Titulo", "La categoría esta registrada");
                }
                else
                {
                    db.Prestamos.Update(prestamo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(prestamo);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Prestamos.Find(id);
            if (cat != null)
            {

                //verifica si el id está presente en producto
                var prod = db.Prestamos.FirstOrDefault(x => x.IdPrestamo == id);
                if (prod == null)
                {
                    return Json("No se puede eliminar porque tiene datos asosiados");
                }
                db.Prestamos.Remove(cat);
                db.SaveChanges();
                //retorna un mensaje en formato json para ser capturado por javascript
                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}

