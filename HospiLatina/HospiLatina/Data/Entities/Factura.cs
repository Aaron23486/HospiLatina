using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente Paciente { get; set; }

        public ICollection<DetalleFactura> DetallesFactura { get; set; }
    }
}
