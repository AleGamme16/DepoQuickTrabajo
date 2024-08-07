using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class DtoRegistroAccion
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string TipoAccion { get; set; }

        [Required]
        public string UsuarioNombre { get; set; }

        [Required]
        public string UsuarioApellido { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }
    }
}
