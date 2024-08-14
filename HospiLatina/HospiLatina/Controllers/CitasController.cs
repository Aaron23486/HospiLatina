using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospiLatina.Data;
using HospiLatina.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospiLatina.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CitasController : Controller
    {
        private readonly DataContext _context;

        public CitasController(DataContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Citas.Include(c => c.Consultorio).Include(c => c.Estudiante).Include(c => c.Paciente).Include(c => c.Procedimiento).Include(c => c.Profesor).Include(c => c.Sala);
            return View(await dataContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Consultorio)
                .Include(c => c.Estudiante)
                .Include(c => c.Paciente)
                .Include(c => c.Procedimiento)
                .Include(c => c.Profesor)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        [Authorize(Roles = "Admin")]
        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdConsultorio"] = new SelectList(
                _context.Consultorios.Select(c => new {
                    c.IdConsultorio,
                    NombreCompleto = $"{c.Nombre} ({c.IdConsultorio})"
                }).ToList(), "IdConsultorio", "NombreCompleto");

            ViewData["IdEstudiante"] = new SelectList(
                _context.Estudiantes.Select(e => new {
                    e.IdEstudiante,
                    NombreCompleto = $"{e.Nombre} {e.Apellido} ({e.IdEstudiante})"
                }).ToList(), "IdEstudiante", "NombreCompleto");

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto");

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(pr => new {
                    pr.IdProcedimiento,
                    NombreCompleto = $"{pr.Nombre} ({pr.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto");

            ViewData["IdProfesor"] = new SelectList(
                _context.Profesores.Select(pr => new {
                    pr.IdProfesor,
                    NombreCompleto = $"{pr.Nombre} {pr.Apellido} ({pr.IdProfesor})"
                }).ToList(), "IdProfesor", "NombreCompleto");

            ViewData["IdSala"] = new SelectList(
                _context.SalasCirugia.Select(s => new {
                    s.IdSala,
                    NombreCompleto = $"{s.Nombre} ({s.IdSala})"
                }).ToList(), "IdSala", "NombreCompleto");

            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdProfesor,IdProcedimiento,IdEstudiante,Fecha,Hora,IdConsultorio,IdPaciente,IdSala")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Rebuild the SelectLists with the selected values
            ViewData["IdConsultorio"] = new SelectList(
                _context.Consultorios.Select(c => new {
                    c.IdConsultorio,
                    NombreCompleto = $"{c.Nombre} ({c.IdConsultorio})"
                }).ToList(), "IdConsultorio", "NombreCompleto", cita.IdConsultorio);

            ViewData["IdEstudiante"] = new SelectList(
                _context.Estudiantes.Select(e => new {
                    e.IdEstudiante,
                    NombreCompleto = $"{e.Nombre} {e.Apellido} ({e.IdEstudiante})"
                }).ToList(), "IdEstudiante", "NombreCompleto", cita.IdEstudiante);

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", cita.IdPaciente);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(pr => new {
                    pr.IdProcedimiento,
                    NombreCompleto = $"{pr.Nombre} ({pr.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", cita.IdProcedimiento);

            ViewData["IdProfesor"] = new SelectList(
                _context.Profesores.Select(pr => new {
                    pr.IdProfesor,
                    NombreCompleto = $"{pr.Nombre} {pr.Apellido} ({pr.IdProfesor})"
                }).ToList(), "IdProfesor", "NombreCompleto", cita.IdProfesor);

            ViewData["IdSala"] = new SelectList(
                _context.SalasCirugia.Select(s => new {
                    s.IdSala,
                    NombreCompleto = $"{s.Nombre} ({s.IdSala})"
                }).ToList(), "IdSala", "NombreCompleto", cita.IdSala);

            return View(cita);
        }

        [Authorize(Roles = "Admin")]
        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Consultorio)
                .Include(c => c.Estudiante)
                .Include(c => c.Paciente)
                .Include(c => c.Procedimiento)
                .Include(c => c.Profesor)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(m => m.IdCita == id);

            if (cita == null)
            {
                return NotFound();
            }

            ViewData["IdConsultorio"] = new SelectList(
                _context.Consultorios.Select(c => new {
                    c.IdConsultorio,
                    NombreCompleto = $"{c.Nombre} ({c.IdConsultorio})"
                }).ToList(), "IdConsultorio", "NombreCompleto", cita.IdConsultorio);

            ViewData["IdEstudiante"] = new SelectList(
                _context.Estudiantes.Select(e => new {
                    e.IdEstudiante,
                    NombreCompleto = $"{e.Nombre} {e.Apellido} ({e.IdEstudiante})"
                }).ToList(), "IdEstudiante", "NombreCompleto", cita.IdEstudiante);

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", cita.IdPaciente);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(pr => new {
                    pr.IdProcedimiento,
                    NombreCompleto = $"{pr.Nombre} ({pr.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", cita.IdProcedimiento);

            ViewData["IdProfesor"] = new SelectList(
                _context.Profesores.Select(pr => new {
                    pr.IdProfesor,
                    NombreCompleto = $"{pr.Nombre} {pr.Apellido} ({pr.IdProfesor})"
                }).ToList(), "IdProfesor", "NombreCompleto", cita.IdProfesor);

            ViewData["IdSala"] = new SelectList(
                _context.SalasCirugia.Select(s => new {
                    s.IdSala,
                    NombreCompleto = $"{s.Nombre} ({s.IdSala})"
                }).ToList(), "IdSala", "NombreCompleto", cita.IdSala);

            return View(cita);
        }

        [Authorize(Roles = "Admin")]
        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdProfesor,IdProcedimiento,IdEstudiante,Fecha,Hora,IdConsultorio,IdPaciente,IdSala")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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

            ViewData["IdConsultorio"] = new SelectList(
                _context.Consultorios.Select(c => new {
                    c.IdConsultorio,
                    NombreCompleto = $"{c.Nombre} ({c.IdConsultorio})"
                }).ToList(), "IdConsultorio", "NombreCompleto", cita.IdConsultorio);

            ViewData["IdEstudiante"] = new SelectList(
                _context.Estudiantes.Select(e => new {
                    e.IdEstudiante,
                    NombreCompleto = $"{e.Nombre} {e.Apellido} ({e.IdEstudiante})"
                }).ToList(), "IdEstudiante", "NombreCompleto", cita.IdEstudiante);

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.IdPaciente,
                    NombreCompleto = $"{p.Nombre} {p.Apellido} ({p.IdPaciente})"
                }).ToList(), "IdPaciente", "NombreCompleto", cita.IdPaciente);

            ViewData["IdProcedimiento"] = new SelectList(
                _context.Procedimientos.Select(pr => new {
                    pr.IdProcedimiento,
                    NombreCompleto = $"{pr.Nombre} ({pr.IdProcedimiento})"
                }).ToList(), "IdProcedimiento", "NombreCompleto", cita.IdProcedimiento);

            ViewData["IdProfesor"] = new SelectList(
                _context.Profesores.Select(pr => new {
                    pr.IdProfesor,
                    NombreCompleto = $"{pr.Nombre} {pr.Apellido} ({pr.IdProfesor})"
                }).ToList(), "IdProfesor", "NombreCompleto", cita.IdProfesor);

            ViewData["IdSala"] = new SelectList(
                _context.SalasCirugia.Select(s => new {
                    s.IdSala,
                    NombreCompleto = $"{s.Nombre} ({s.IdSala})"
                }).ToList(), "IdSala", "NombreCompleto", cita.IdSala);

            return View(cita);
        }

        [Authorize(Roles = "Admin")]
        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Consultorio)
                .Include(c => c.Estudiante)
                .Include(c => c.Paciente)
                .Include(c => c.Procedimiento)
                .Include(c => c.Profesor)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        [Authorize(Roles = "Admin")]
        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
