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
    public class FacturasController : Controller
    {
        private readonly DataContext _context;

        public FacturasController(DataContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Facturas
                .Include(f => f.Paciente)
                .Include(f => f.DetallesFactura)  // Incluye la relación con DetallesFactura
                    .ThenInclude(d => d.Procedimiento);  // Incluye la relación con Procedimiento
            return View(await dataContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Paciente)
                .Include(f => f.DetallesFactura)  // Incluye los detalles de la factura
                    .ThenInclude(d => d.Procedimiento)  // Incluye el procedimiento en los detalles
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,IdPaciente,Fecha,Total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", factura.IdPaciente);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.DetallesFactura)  // Incluye los detalles de la factura
                    .ThenInclude(d => d.Procedimiento)  // Incluye el procedimiento en los detalles
                .FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null)
            {
                return NotFound();
            }

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", factura.IdPaciente);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        // POST: Facturas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,IdPaciente,Fecha,Total")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
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
            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", factura.IdPaciente);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Paciente)
                .Include(f => f.DetallesFactura)  // Incluye los detalles de la factura
                    .ThenInclude(d => d.Procedimiento)  // Incluye el procedimiento en los detalles
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.IdFactura == id);
        }
    }
}
