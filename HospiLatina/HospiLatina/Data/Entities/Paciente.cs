using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        [Display(Name = "Paciente")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public ICollection<Cita>? Citas { get; set; }
        public ICollection<Factura>? Facturas { get; set; }
    }
}
