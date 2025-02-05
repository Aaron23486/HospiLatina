﻿using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class Consultorio
    {
        [Key]
        public int IdConsultorio { get; set; }
        public string Ubicacion { get; set; }
        [Display(Name = "Consultorio")]
        public string Nombre { get; set; }

        public ICollection<Cita>? Citas { get; set; }
    }
}
