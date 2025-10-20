using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMetropolis.Models;

namespace BibliotecaMetropolis.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly BibliotecaContext _context;

        public EtiquetasController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Etiquetas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Etiquetas.ToListAsync());
        }

        // GET: Etiquetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etiqueta = await _context.Etiquetas
                .FirstOrDefaultAsync(m => m.IdEtiqueta == id);
            if (etiqueta == null)
            {
                return NotFound();
            }

            return View(etiqueta);
        }

        // GET: Etiquetas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etiquetas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEtiqueta,Nombre")] Etiqueta etiqueta)
        {
            // --- LÍNEA AÑADIDA PARA LA SOLUCIÓN ---
            ModelState.Remove(nameof(Etiqueta.RecursoEtiquetas));

            if (ModelState.IsValid)
            {
                _context.Add(etiqueta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etiqueta);
        }

        // GET: Etiquetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etiqueta = await _context.Etiquetas.FindAsync(id);
            if (etiqueta == null)
            {
                return NotFound();
            }
            return View(etiqueta);
        }

        // POST: Etiquetas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEtiqueta,Nombre")] Etiqueta etiqueta)
        {
            if (id != etiqueta.IdEtiqueta)
            {
                return NotFound();
            }

            // --- LÍNEA AÑADIDA PARA LA SOLUCIÓN ---
            ModelState.Remove(nameof(Etiqueta.RecursoEtiquetas));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etiqueta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtiquetaExists(etiqueta.IdEtiqueta))
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
            return View(etiqueta);
        }

        // GET: Etiquetas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etiqueta = await _context.Etiquetas
                .FirstOrDefaultAsync(m => m.IdEtiqueta == id);
            if (etiqueta == null)
            {
                return NotFound();
            }

            return View(etiqueta);
        }

        // POST: Etiquetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etiqueta = await _context.Etiquetas.FindAsync(id);
            if (etiqueta != null)
            {
                _context.Etiquetas.Remove(etiqueta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtiquetaExists(int id)
        {
            return _context.Etiquetas.Any(e => e.IdEtiqueta == id);
        }
    }
}