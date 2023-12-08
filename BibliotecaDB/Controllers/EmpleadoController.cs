using BibliotecaDB.Helper;
using BibliotecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaDB.Controllers
{
    public class EmpleadoController : Controller
    {
        private BibliotecaDbContext db = new();
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Empleado empleado)
        {
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado");
            if (ModelState.IsValid)
            {
                var p = db.Empleados.Find(empleado.IdEmpleado);
                if (p != null)
                {
                    ModelState.AddModelError("IdEmpleado", "ya esta registrado");
                    return View();
                }
                db.Empleados.Add(empleado);
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
            var empleado = from c in db.Empleados select c;
            if (!string.IsNullOrEmpty(buscar))
            {
                empleado = empleado.Where(x => x.Nombre.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2;
            return View(await PaginatedList<Empleado>.CreateAsync(empleado, numPag ?? 1, tamPag));
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                var cat = db.Empleados.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Empleados.FirstOrDefault(
                    x => x.IdEmpleado != empleado.IdEmpleado &&
                    x.Nombre == empleado.Nombre &&
                    x.Apellido == empleado.Apellido &&
                    x.Cargo == empleado.Cargo);
                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "La categoría esta registrada");
                }
                else
                {
                    db.Empleados.Update(empleado);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(empleado);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Empleados.Find(id);
            if (cat != null)
            {

                db.Empleados.Remove(cat);
                db.SaveChanges();

                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}
