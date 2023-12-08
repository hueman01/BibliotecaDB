using BibliotecaDB.Helper;
using BibliotecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaDB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ClienteController : Controller
    {
        private BibliotecaDbContext db = new();
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cliente cli)
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente");
            if (ModelState.IsValid)
            {
                var p = db.Clientes.Find(cli.IdCliente);
                if (p != null)
                {
                    ModelState.AddModelError("IdCliente", "ya esta registrado");
                    return View();
                }
                db.Clientes.Add(cli);
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
            var cli = from c in db.Clientes select c;
            if (!string.IsNullOrEmpty(buscar))
            {
                cli = cli.Where(x => x.Nombre.ToLower().Contains(buscar.ToLower()));
            }
            int tamPag = 2;
            return View(await PaginatedList<Cliente>.CreateAsync(cli, numPag ?? 1, tamPag));
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                var cat = db.Clientes.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Clientes.FirstOrDefault(
                    x => x.IdCliente != cliente.IdCliente &&
                    x.Nombre == cliente.Nombre &&
                    x.Apellido == cliente.Apellido &&
                    x.Direccion == cliente.Direccion &&
                    x.CorreoElectronico == cliente.CorreoElectronico &&
                    x.Telefono == cliente.Telefono);
                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "La categoría esta registrada");
                }
                else
                {
                    db.Clientes.Update(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }
        public IActionResult Delete(int id)
        {
            var cat = db.Clientes.Find(id);
            if (cat != null)
            {

                db.Clientes.Remove(cat);
                db.SaveChanges();

                return Json("ok");
            }
            return RedirectToAction("Index");
        }
    }
}

