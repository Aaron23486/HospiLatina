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

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdConsultorio"] = new SelectList(_context.Consultorios, "IdConsultorio", "IdConsultorio");
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente");
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento");
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor");
            ViewData["IdSala"] = new SelectList(_context.SalasCirugia, "IdSala", "IdSala");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["IdConsultorio"] = new SelectList(_context.Consultorios, "IdConsultorio", "IdConsultorio", cita.IdConsultorio);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cita.IdEstudiante);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", cita.IdPaciente);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", cita.IdProcedimiento);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", cita.IdProfesor);
            ViewData["IdSala"] = new SelectList(_context.SalasCirugia, "IdSala", "IdSala", cita.IdSala);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdConsultorio"] = new SelectList(_context.Consultorios, "IdConsultorio", "IdConsultorio", cita.IdConsultorio);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cita.IdEstudiante);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", cita.IdPaciente);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", cita.IdProcedimiento);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", cita.IdProfesor);
            ViewData["IdSala"] = new SelectList(_context.SalasCirugia, "IdSala", "IdSala", cita.IdSala);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["IdConsultorio"] = new SelectList(_context.Consultorios, "IdConsultorio", "IdConsultorio", cita.IdConsultorio);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cita.IdEstudiante);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", cita.IdPaciente);
            ViewData["IdProcedimiento"] = new SelectList(_context.Procedimientos, "IdProcedimiento", "IdProcedimiento", cita.IdProcedimiento);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", cita.IdProfesor);
            ViewData["IdSala"] = new SelectList(_context.SalasCirugia, "IdSala", "IdSala", cita.IdSala);
            return View(cita);
        }

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
