using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DtoDeposito
    {
        public int ID { get; set; }
        [Required]
        public EnumArea Area { get; set; }

        [Required]
        public EnumTamano Tamano { get; set; }

        [Required]
        public bool Climatizado { get; set; }

        public int? PromocionId { get; set; }
        public string Nombre { get; set; }

        public List<DtoDisponibilidad> Disponibilidades { get; set; }

        public override string ToString()
        {
            return $" {Nombre} - {Area} - {Tamano} - Climatizado: {(Climatizado ? "Sí" : "No")}";
        }
    }

   
}
