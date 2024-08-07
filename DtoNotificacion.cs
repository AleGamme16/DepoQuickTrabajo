using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DtoNotificacion
    {
        public int ID {  get; set; }
        public string Mensaje {  get; set; }
        public Usuario Usuario {  get; set; }
        public Reserva Reserva { get; set; }
        public DateTime Fecha { get; set; }
    }
}
