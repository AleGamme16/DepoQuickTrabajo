using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DtoReserva
    {

        public int ID { get; set; }
        [Required]
        public int ClienteID { get; set; }

        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a 0.")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar un deposito")]
        public int DepositoID { get; set; }

        [StringLength(300, ErrorMessage = "El motivo de rechazo no debe superar los 300 caracteres.")]
        public string? MotivoRechazo { get; set; }

        
        public EnumEstado Estado { get; set; }

        public EnumTamano TamanoDeposito { get; set; }

        public EnumEstadoPago? EstadoPago { get; set; }

    }
}
