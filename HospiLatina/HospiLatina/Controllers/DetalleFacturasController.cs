using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospiLatina.Data;
using HospiLatina.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HospiLatina.Controllers
{
    [Authorize(Roles = "User,Admin")]
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
            var dataContext = _context.DetallesFactura
                .Include(d => d.Factura)
                    .ThenInclude(f => f.Paciente)  // Incluye la relación con Paciente
                .Include(d => d.Procedimiento);
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
                    .ThenInclude(f => f.Paciente)  // Incluye la relación con Paciente
                .Include(d => d.Procedimiento)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        [Authorize(Roles = "Admin")]
        // GET: DetalleFacturas/Create
        public IActionResult Create()
        {
            ViewData["IdFactura"] = new SelectList(
                _context.Facturas.Select(f => new {
                    f.IdFactura,
                    NombreCompleto = $"{f.Paciente.Nombre} {f.Paciente.Apellido} ({f.IdFactura})"
                }).ToList(), "IdFactura", "NombreCompleto");

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(p => new {
                    p.IdProcedimiento,
                    NombreCompleto = $"{p.Nombre} ({p.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto");

            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: DetalleFacturas/Create
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
            ViewData["IdFactura"] = new SelectList(
                _context.Facturas.Select(f => new {
                    f.IdFactura,
                    NombreCompleto = $"{f.Paciente.Nombre} {f.Paciente.Apellido} ({f.IdFactura})"
                }).ToList(), "IdFactura", "NombreCompleto", detalleFactura.IdFactura);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(p => new {
                    p.IdProcedimiento,
                    NombreCompleto = $"{p.Nombre} ({p.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", detalleFactura.IdProcedimiento);

            return View(detalleFactura);
        }

        [Authorize(Roles = "Admin")]
        // GET: DetalleFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                    .ThenInclude(f => f.Paciente)  // Incluye la relación con Paciente
                .Include(d => d.Procedimiento)
                .FirstOrDefaultAsync(d => d.IdDetalle == id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            ViewData["IdFactura"] = new SelectList(
                _context.Facturas.Select(f => new {
                    f.IdFactura,
                    NombreCompleto = $"{f.Paciente.Nombre} {f.Paciente.Apellido} ({f.IdFactura})"
                }).ToList(), "IdFactura", "NombreCompleto", detalleFactura.IdFactura);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(p => new {
                    p.IdProcedimiento,
                    NombreCompleto = $"{p.Nombre} ({p.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", detalleFactura.IdProcedimiento);

            return View(detalleFactura);
        }

        [Authorize(Roles = "Admin")]
        // POST: DetalleFacturas/Edit/5
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
            ViewData["IdFactura"] = new SelectList(
                _context.Facturas.Select(f => new {
                    f.IdFactura,
                    NombreCompleto = $"{f.Paciente.Nombre} {f.Paciente.Apellido} ({f.IdFactura})"
                }).ToList(), "IdFactura", "NombreCompleto", detalleFactura.IdFactura);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(p => new {
                    p.IdProcedimiento,
                    NombreCompleto = $"{p.Nombre} ({p.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", detalleFactura.IdProcedimiento);

            return View(detalleFactura);
        }

        [Authorize(Roles = "Admin")]
        // GET: DetalleFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                    .ThenInclude(f => f.Paciente)  // Incluye la relación con Paciente
                .Include(d => d.Procedimiento)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        [Authorize(Roles = "Admin")]
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
