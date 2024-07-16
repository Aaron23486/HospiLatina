using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class DetalleFactura
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public int IdProcedimiento { get; set; }
        public double Subtotal { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("IdFactura")]
        public Factura? Factura { get; set; }

        [ForeignKey("IdProcedimiento")]
        public Procedimiento? Procedimiento { get; set; }
    }
}
