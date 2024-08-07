using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DtoUsuario
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public int ID { get; set; }

        [Required]
        public EnumRol Rol { get; set; }
    }
}
