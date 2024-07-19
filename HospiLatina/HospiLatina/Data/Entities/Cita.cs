using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public int IdProfesor { get; set; }
        public int IdProcedimiento { get; set; }
        public int IdEstudiante { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int IdConsultorio { get; set; }
        public int IdPaciente { get; set; }
        public int IdSala { get; set; }

        [ForeignKey("IdProfesor")]
        public Profesor? Profesor { get; set; }

        [ForeignKey("IdProcedimiento")]
        public Procedimiento? Procedimiento { get; set; }

        [ForeignKey("IdEstudiante")]
        public Estudiante? Estudiante { get; set; }

        [ForeignKey("IdConsultorio")]
        public Consultorio? Consultorio { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; }

        [ForeignKey("IdSala")]
        public SalaCirugia? Sala { get; set; }
    }
}
