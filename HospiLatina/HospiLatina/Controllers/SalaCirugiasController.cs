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
    public class SalaCirugiasController : Controller
    {
        private readonly DataContext _context;

        public SalaCirugiasController(DataContext context)
        {
            _context = context;
        }

        // GET: SalaCirugias
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalasCirugia.ToListAsync());
        }

        // GET: SalaCirugias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCirugia = await _context.SalasCirugia
                .FirstOrDefaultAsync(m => m.IdSala == id);
            if (salaCirugia == null)
            {
                return NotFound();
            }

            return View(salaCirugia);
        }

        // GET: SalaCirugias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalaCirugias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSala,Nombre,Ubicacion")] SalaCirugia salaCirugia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaCirugia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salaCirugia);
        }

        // GET: SalaCirugias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCirugia = await _context.SalasCirugia.FindAsync(id);
            if (salaCirugia == null)
            {
                return NotFound();
            }
            return View(salaCirugia);
        }

        // POST: SalaCirugias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSala,Nombre,Ubicacion")] SalaCirugia salaCirugia)
        {
            if (id != salaCirugia.IdSala)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaCirugia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaCirugiaExists(salaCirugia.IdSala))
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
            return View(salaCirugia);
        }

        // GET: SalaCirugias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCirugia = await _context.SalasCirugia
                .FirstOrDefaultAsync(m => m.IdSala == id);
            if (salaCirugia == null)
            {
                return NotFound();
            }

            return View(salaCirugia);
        }

        // POST: SalaCirugias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaCirugia = await _context.SalasCirugia.FindAsync(id);
            if (salaCirugia != null)
            {
                _context.SalasCirugia.Remove(salaCirugia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaCirugiaExists(int id)
        {
            return _context.SalasCirugia.Any(e => e.IdSala == id);
        }
    }
}
