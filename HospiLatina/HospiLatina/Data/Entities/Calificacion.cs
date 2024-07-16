using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Calificacion
    {
        [Key]
        public int IdCalificacion { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
        public int IdCita { get; set; }

        [ForeignKey("IdCita")]
        public Cita Cita { get; set; }
    }
}
