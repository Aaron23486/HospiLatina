using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospiLatina.Data;
using HospiLatina.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospiLatina.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CalificacionesController : Controller
    {
        private readonly DataContext _context;

        public CalificacionesController(DataContext context)
        {
            _context = context;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Calificaciones.Include(c => c.Cita);
            return View(await dataContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Cita)
                .FirstOrDefaultAsync(m => m.IdCalificacion == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        [Authorize(Roles = "Admin")]
        // GET: Calificaciones/Create
        public IActionResult Create()
        {
            ViewData["IdCita"] = new SelectList(
                _context.Citas.Select(c => new {
                    c.IdCita,
                    NombreCompleto = $"{c.Fecha} - {c.Hora} ({c.IdCita})"
                }).ToList(), "IdCita", "NombreCompleto");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Calificaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCalificacion,Puntuacion,Comentario,IdCita")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCita"] = new SelectList(
                _context.Citas.Select(c => new {
                    c.IdCita,
                    NombreCompleto = $"{c.Fecha} - {c.Hora} ({c.IdCita})"
                }).ToList(), "IdCita", "NombreCompleto", calificacion.IdCita);
            return View(calificacion);
        }

        [Authorize(Roles = "Admin")]
        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            ViewData["IdCita"] = new SelectList(
                _context.Citas.Select(c => new {
                    c.IdCita,
                    NombreCompleto = $"{c.Fecha} - {c.Hora} ({c.IdCita})"
                }).ToList(), "IdCita", "NombreCompleto", calificacion.IdCita);
            return View(calificacion);
        }

        [Authorize(Roles = "Admin")]
        // POST: Calificaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCalificacion,Puntuacion,Comentario,IdCita")] Calificacion calificacion)
        {
            if (id != calificacion.IdCalificacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.IdCalificacion))
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
            ViewData["IdCita"] = new SelectList(
                _context.Citas.Select(c => new {
                    c.IdCita,
                    NombreCompleto = $"{c.Fecha} - {c.Hora} ({c.IdCita})"
                }).ToList(), "IdCita", "NombreCompleto", calificacion.IdCita);
            return View(calificacion);
        }

        [Authorize(Roles = "Admin")]
        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Cita)
                .FirstOrDefaultAsync(m => m.IdCalificacion == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        [Authorize(Roles = "Admin")]
        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificaciones.Remove(calificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificaciones.Any(e => e.IdCalificacion == id);
        }
    }
}
