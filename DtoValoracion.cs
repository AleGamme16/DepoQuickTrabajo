using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend;

namespace Backend.DTOs
{
    public class DtoValoracion
    {
        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public int DepositoID { get; set; }

        [Required]
        public int ValoracionID { get; set; }

        [Required]
        [StringLength(501)]
        public string Comentario { get; set; }

        [Required]
        public int Estrellas { get; set; }
        public EnumArea AreaDeposito { get; set; }

        public EnumTamano TamanoDeposito { get; set; }

        public bool DepositoClimatizado { get; set; }

    }
}

