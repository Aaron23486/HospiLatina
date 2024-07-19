using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Profesor
    {
        [Key]
        public int IdProfesor { get; set; }

        [Display(Name = "Profesor")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }

        public ICollection<Cita>? Citas { get; set; }
    }
}
