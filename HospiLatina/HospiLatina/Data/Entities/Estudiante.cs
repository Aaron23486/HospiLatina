using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int AnoDeGraduacion { get; set; }
        public string HorarioAtencion { get; set; }

        public ICollection<Cita>? Citas { get; set; }
    }
}
