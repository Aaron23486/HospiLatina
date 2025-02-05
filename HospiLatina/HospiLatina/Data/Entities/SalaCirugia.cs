﻿using System.ComponentModel.DataAnnotations;

namespace HospiLatina.Data.Entities
{
    public class SalaCirugia
    {
        [Key]
        public int IdSala { get; set; }

        [Display(Name = "Sala")]
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public ICollection<Cita>? Citas { get; set; }
    }
}
