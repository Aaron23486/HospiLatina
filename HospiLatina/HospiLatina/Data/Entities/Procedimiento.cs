using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Procedimiento
    {
        [Key]
        public int IdProcedimiento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public ICollection<Cita>? Citas { get; set; }
        public ICollection<DetalleFactura>? DetallesFactura { get; set; }
    }
}
