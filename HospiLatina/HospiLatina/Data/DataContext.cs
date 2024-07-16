using HospiLatina.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospiLatina.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Procedimiento> Procedimientos { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<SalaCirugia> SalasCirugia { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
    }
}
