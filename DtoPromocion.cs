using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class DtoPromocion
    {
        [Required]
        [StringLength(20, ErrorMessage = "La etiqueta no puede tener más de 20 caracteres.")]
        public string Etiqueta { get; set; }

        [Required]
        [Range(0.05, 0.75, ErrorMessage = "El descuento debe estar entre 5% y 75%.")]
        public decimal Descuento { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Desde { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Hasta { get; set; }

        [Required]
        public int ID { get; set; }
    }
}
