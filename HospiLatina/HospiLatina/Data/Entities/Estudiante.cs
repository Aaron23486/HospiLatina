using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }
        [Display(Name = "Estudiante")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }

        [Display(Name = "Ano De Graduacion")]
        public int AnoDeGraduacion { get; set; }

        [Display(Name = "Horario Atencion")]
        public string HorarioAtencion { get; set; }

        public ICollection<Cita>? Citas { get; set; }
    }
}
