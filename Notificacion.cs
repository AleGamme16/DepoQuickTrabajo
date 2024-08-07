using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Notificacion
    {
        public int ID { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public Reserva Reserva { get; set; }
        public DateTime Fecha { get; set; }

        public Notificacion(int id, string mensaje, Usuario usuario, Reserva reserva, DateTime fecha) {
            ID = id;
            Mensaje = mensaje;
            Usuario = usuario;
            Reserva = reserva;
            Fecha = fecha;
        }

        public Notificacion() { }
    }
}
