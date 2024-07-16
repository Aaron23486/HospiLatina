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
    public class DetalleFacturasController : Controller
    {
        private readonly DataContext _context;

        public DetalleFacturasController(DataContext context)
        {
            _context = context;
        }

        // GET: DetalleFacturas
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.DetallesFactura.Include(d => d.Factura).Include(d => d.Procedimiento);
            return View(await dataContext.ToListAsync());
        }

        // GET: DetalleFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                .Include(d => d.Procedimiento)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Create
        public IActionResult Create()
        {
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura");
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento");
            return View();
        }

        // POST: DetalleFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalle,IdFactura,IdProcedimiento,Subtotal,Cantidad")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleFactura.IdFactura);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", detalleFactura.IdProcedimiento);
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetallesFactura.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleFactura.IdFactura);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", detalleFactura.IdProcedimiento);
            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalle,IdFactura,IdProcedimiento,Subtotal,Cantidad")] DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleFacturaExists(detalleFactura.IdDetalle))
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
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleFactura.IdFactura);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", detalleFactura.IdProcedimiento);
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                .Include(d => d.Procedimiento)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleFactura = await _context.DetallesFactura.FindAsync(id);
            if (detalleFactura != null)
            {
                _context.DetallesFactura.Remove(detalleFactura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetallesFactura.Any(e => e.IdDetalle == id);
        }
    }
}
