using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova1Bimestre.Data;
using Prova1Bimestre.Models;

namespace Prova1Bimestre.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppCont _appCont;

        public ClienteController(AppCont appCont)
        {
            _appCont = appCont;
        }

        //Action sincrona
        public IActionResult Index()
        {
            var allTasks = _appCont.Clientes.ToList();
            return View(allTasks);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(long? id) // O "?" é para ele aceitar os valores nulos, para não dar B.O no código
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _appCont.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _appCont.Add(cliente);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _appCont.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appCont.Update(cliente);
                    await _appCont.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExist(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cadastros/Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _appCont.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cadastros/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _appCont.Clientes.FindAsync(id);
            _appCont.Clientes.Remove(cliente);
            await _appCont.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExist(long id)
        {
            return _appCont.Clientes.Any(e => e.Id == id);
        }
    }
}
