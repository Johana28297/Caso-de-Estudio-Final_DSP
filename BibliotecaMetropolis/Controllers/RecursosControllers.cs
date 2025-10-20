using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMetropolis.Models;

namespace BibliotecaMetropolis.Controllers
{
    public class RecursosController : Controller
    {
        private readonly BibliotecaContext _context;

        public RecursosController(BibliotecaContext context)
        {
            _context = context;
        }

        // (Los métodos GET: Index, Details, Create, Edit, Delete se quedan igual)
        #region Métodos GET (Sin Cambios)
        // GET: Recursos
        public async Task<IActionResult> Index(string searchString)
        {
            var recursos = from r in _context.Recursos.Include(r => r.Editorial).Include(r => r.Pais)
                           select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recursos = recursos.Where(s => s.Titulo.Contains(searchString)
                                           || (s.PalabrasBusqueda != null && s.PalabrasBusqueda.Contains(searchString)));
            }

            return View(await recursos.ToListAsync());
        }

        // GET: Recursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .Include(r => r.Editorial)
                .Include(r => r.Pais)
                .Include(r => r.RecursoEtiquetas)
                    .ThenInclude(re => re.Etiqueta)
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (recurso == null)
            {
                return NotFound();
            }

            ViewData["IdEtiqueta"] = new SelectList(_context.Etiquetas, "IdEtiqueta", "Nombre");

            return View(recurso);
        }

        // GET: Recursos/Create
        public IActionResult Create()
        {
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre");
            return View();
        }

        // GET: Recursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            return View(recurso);
        }

        // GET: Recursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .Include(r => r.Editorial)
                .Include(r => r.Pais)
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }
        #endregion

        // --- MÉTODO POST CREATE CON LA CORRECCIÓN FINAL ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRec,Titulo,AnnoPublic,Edicion,Cantidad,Precio,PalabrasBusqueda,TipoRecurso,InstitucionEducativa,Ciudad,FechaRegistro,IdEdit,IdPais")] Recurso recurso)
        {
            // Ignoramos las colecciones que no vienen del formulario
            ModelState.Remove(nameof(Recurso.AutoresRecursos));
            ModelState.Remove(nameof(Recurso.RecursoEtiquetas));

            // --- LÍNEAS AÑADIDAS PARA LA SOLUCIÓN FINAL ---
            // Ignoramos los objetos de navegación, ya que solo nos interesan sus IDs
            ModelState.Remove(nameof(Recurso.Editorial));
            ModelState.Remove(nameof(Recurso.Pais));

            if (ModelState.IsValid)
            {
                if (recurso.FechaRegistro == default)
                {
                    recurso.FechaRegistro = DateTime.Now;
                }

                _context.Add(recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, recargamos las listas
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            return View(recurso);
        }

        // --- MÉTODO POST EDIT CON LA CORRECCIÓN FINAL ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRec,Titulo,AnnoPublic,Edicion,Cantidad,Precio,PalabrasBusqueda,TipoRecurso,InstitucionEducativa,Ciudad,FechaRegistro,IdEdit,IdPais")] Recurso recurso)
        {
            if (id != recurso.IdRec)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(Recurso.AutoresRecursos));
            ModelState.Remove(nameof(Recurso.RecursoEtiquetas));

            // --- LÍNEAS AÑADIDAS PARA LA SOLUCIÓN FINAL ---
            ModelState.Remove(nameof(Recurso.Editorial));
            ModelState.Remove(nameof(Recurso.Pais));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursoExists(recurso.IdRec))
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
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            return View(recurso);
        }

        #region Métodos de Borrado y Etiquetas (Sin Cambios)
        // POST: Recursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso != null)
            {
                _context.Recursos.Remove(recurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursoExists(int id)
        {
            return _context.Recursos.Any(e => e.IdRec == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarEtiqueta(int IdRec, int IdEtiqueta)
        {
            var yaExiste = await _context.RecursoEtiquetas
                .AnyAsync(re => re.IdRec == IdRec && re.IdEtiqueta == IdEtiqueta);

            if (!yaExiste)
            {
                var recursoEtiqueta = new RecursoEtiqueta
                {
                    IdRec = IdRec,
                    IdEtiqueta = IdEtiqueta
                };
                _context.RecursoEtiquetas.Add(recursoEtiqueta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = IdRec });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuitarEtiqueta(int id)
        {
            var recursoEtiqueta = await _context.RecursoEtiquetas.FindAsync(id);
            if (recursoEtiqueta != null)
            {
                var idRecurso = recursoEtiqueta.IdRec;
                _context.RecursoEtiquetas.Remove(recursoEtiqueta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = idRecurso });
            }
            return NotFound();
        }
        #endregion
    }
}