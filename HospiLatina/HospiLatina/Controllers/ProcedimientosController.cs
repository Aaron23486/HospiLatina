using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospiLatina.Data;
using HospiLatina.Data.Entities;

namespace HospiLatina.Controllers
{
    public class ProcedimientosController : Controller
    {
        private readonly DataContext _context;

        public ProcedimientosController(DataContext context)
        {
            _context = context;
        }

        // GET: Procedimientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Procedimientos.ToListAsync());
        }

        // GET: Procedimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimientos
                .FirstOrDefaultAsync(m => m.IdProcedimiento == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // GET: Procedimientos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procedimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProcedimiento,Nombre,Descripcion,Precio")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedimiento);
        }

        // GET: Procedimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimientos.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }
            return View(procedimiento);
        }

        // POST: Procedimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProcedimiento,Nombre,Descripcion,Precio")] Procedimiento procedimiento)
        {
            if (id != procedimiento.IdProcedimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoExists(procedimiento.IdProcedimiento))
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
            return View(procedimiento);
        }

        // GET: Procedimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimientos
                .FirstOrDefaultAsync(m => m.IdProcedimiento == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // POST: Procedimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedimiento = await _context.Procedimientos.FindAsync(id);
            if (procedimiento != null)
            {
                _context.Procedimientos.Remove(procedimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoExists(int id)
        {
            return _context.Procedimientos.Any(e => e.IdProcedimiento == id);
        }
    }
}
