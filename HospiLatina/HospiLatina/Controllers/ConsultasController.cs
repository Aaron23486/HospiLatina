using Microsoft.AspNetCore.Mvc;
using HospiLatina.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HospiLatina.Data.Entities;

namespace HospiLatina.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly DataContext _context;

        public ConsultasController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Consulta de Horarios Disponibles por Especialidad y Doctor
        public async Task<IActionResult> HorariosDisponibles()
        {
            var horariosDisponibles = await _context.Estudiantes
                .Join(
                    _context.Profesores,
                    e => e.Especialidad, // Assuming `Especialidad` is a string in `Estudiantes`
                    p => p.Especialidad,
                    (e, p) => new
                    {
                        e.HorarioAtencion,
                        Especialidad = p.Especialidad,
                        Doctor = p.Nombre
                    })
                .ToListAsync();

            return View(horariosDisponibles);
        }


        // Cantidad de Pacientes Atendidos por Doctor
        public async Task<IActionResult> PacientesPorDoctor()
        {
            var pacientesPorDoctor = await _context.Citas
                .Include(c => c.Profesor)
                .GroupBy(c => c.Profesor.Nombre)
                .Select(g => new
                {
                    Doctor = g.Key,
                    CantidadPacientes = g.Count()
                })
                .OrderByDescending(g => g.CantidadPacientes)
                .ToListAsync();

            return View(pacientesPorDoctor);
        }


        // Top 5 de Doctores con Más Pacientes Atendidos
        public async Task<IActionResult> Top5DoctoresPacientes()
        {
            var topDoctores = await _context.Citas
                .Include(c => c.Profesor)
                .GroupBy(c => c.Profesor.Nombre)
                .Select(g => new
                {
                    Doctor = g.Key,
                    CantidadPacientes = g.Count()
                })
                .OrderByDescending(g => g.CantidadPacientes)
                .Take(5)
                .ToListAsync();

            return View(topDoctores);
        }


        // Top 3 de Doctores Mejor Calificados
        public async Task<IActionResult> Top3DoctoresCalificados()
        {
            var topCalificados = await _context.Calificaciones
                .Include(cal => cal.Cita)
                .ThenInclude(c => c.Profesor)
                .GroupBy(cal => cal.Cita.Estudiante.Nombre)
                .Select(g => new
                {
                    Doctor = g.Key,
                    PromedioCalificacion = g.Average(cal => cal.Puntuacion)
                })
                .OrderByDescending(g => g.PromedioCalificacion)
                .Take(3)
                .ToListAsync();

            return View(topCalificados);
        }


        // Lista de Servicios Ordenados por los Más Solicitados
        public async Task<IActionResult> ServiciosMasSolicitados()
        {
            var servicios = await _context.Procedimientos
                .Select(p => new
                {
                    Servicio = p.Nombre,
                    VecesSolicitado = p.Citas.Count()
                })
                .OrderByDescending(p => p.VecesSolicitado)
                .ToListAsync();

            return View(servicios);
        }


        // Top 2 de Horarios con Más Pacientes Atendidos
        public async Task<IActionResult> Top2HorariosPacientes()
        {
            var topHorarios = await _context.Citas
                .GroupBy(c => new { c.Fecha, c.Hora })
                .Select(g => new
                {
                    Dia = g.Key.Fecha,
                    Hora = g.Key.Hora,
                    CantidadPacientes = g.Count()
                })
                .OrderByDescending(g => g.CantidadPacientes)
                .Take(2)
                .ToListAsync();

            return View(topHorarios);
        }


        // Top 10 de Servicios que Más Ganancias Generaron en el Mes
        public async Task<IActionResult> Top10ServiciosGanancias(int? mes, int? year)
        {
            if (!mes.HasValue || !year.HasValue)
            {
                mes = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }

            var fechaInicio = new DateTime(year.Value, mes.Value, 1);
            var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

            var query = _context.DetallesFactura
                .Include(df => df.Procedimiento)
                .Include(df => df.Factura)
                .Where(df => df.Factura.Fecha >= fechaInicio && df.Factura.Fecha <= fechaFin)
                .AsQueryable();

            var topServicios = await query
                .GroupBy(df => df.Procedimiento.Nombre)
                .Select(g => new
                {
                    Servicio = g.Key,
                    Ganancias = g.Sum(df => df.Factura.Total)  // Aquí se suman los totales de las facturas
                })
                .OrderByDescending(s => s.Ganancias)
                .Take(10)
                .ToListAsync();

            ViewBag.Mes = mes;
            ViewBag.Year = year;

            return View(topServicios);
        }







        // Acción para mostrar el formulario y listar equipos según el mes y año seleccionados
        [HttpGet]
        public IActionResult EquiposSoporteMes()
        {
            // Mostrar el formulario inicialmente sin resultados
            return View(new List<Equipo>());
        }

        // Acción para procesar la selección del mes y año
        [HttpPost]
        public async Task<IActionResult> EquiposSoporteMes(int mes, int year)
        {
            // Validar que el mes esté entre 1 y 12
            if (mes < 1 || mes > 12)
            {
                ModelState.AddModelError("", "El valor del mes debe estar entre 1 y 12.");
                return View(new List<Equipo>());
            }

            try
            {
                var fechaInicio = new DateTime(year, mes, 1);
                var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

                var equipos = await _context.Equipos
                    .Where(e => e.FechaSoporte >= fechaInicio && e.FechaSoporte <= fechaFin)
                    .ToListAsync();

                return View(equipos);
            }
            catch (ArgumentOutOfRangeException)
            {
                ModelState.AddModelError("", "El año o mes proporcionado es inválido.");
                return View(new List<Equipo>());
            }
        }






        // Listado de Doctores con sus Doctores de Apoyo por Especialidad
        public async Task<IActionResult> DoctoresConApoyo()
        {
            var doctores = await _context.Profesores
                .Include(d => d.Citas)
                .ThenInclude(c => c.Estudiante)
                .ToListAsync();

            var result = doctores.Select(d => new
            {
                Doctor = d.Nombre,
                Especialidad = d.Especialidad,
                DoctoresDeApoyo = d.Citas.Select(c => c.Estudiante).Distinct().ToList()
            }).ToList();

            return View(result);
        }





        // Días de la Semana y Horarios con Menos Pacientes Atendidos en Promedio
        public async Task<IActionResult> HorariosMenosPacientes()
        {
            var citas = await _context.Citas
                .AsNoTracking()
                .ToListAsync();

            var horarios = citas
                .GroupBy(c => new { c.Fecha.DayOfWeek, c.Hora })
                .Select(g => new
                {
                    DiaSemana = g.Key.DayOfWeek,
                    Horario = g.Key.Hora,
                    PromedioPacientes = g.Count() / g.Select(c => c.Fecha.Date).Distinct().Count()
                })
                .OrderBy(g => g.PromedioPacientes)
                .ToList()
                .Select(h => new
                {
                    DiaSemana = ConvertirDiaSemana(h.DiaSemana),
                    h.Horario,
                    h.PromedioPacientes
                });

            return View(horarios);
        }

        // Método para convertir el día de la semana al español
        private string ConvertirDiaSemana(DayOfWeek dia)
        {
            return dia switch
            {
                DayOfWeek.Monday => "Lunes",
                DayOfWeek.Tuesday => "Martes",
                DayOfWeek.Wednesday => "Miércoles",
                DayOfWeek.Thursday => "Jueves",
                DayOfWeek.Friday => "Viernes",
                DayOfWeek.Saturday => "Sábado",
                DayOfWeek.Sunday => "Domingo",
                _ => "Desconocido",
            };
        }


    }
}
