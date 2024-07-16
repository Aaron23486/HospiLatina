using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Equipo
    {
        [Key]
        public int IdEquipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSoporte { get; set; }
    }
}
