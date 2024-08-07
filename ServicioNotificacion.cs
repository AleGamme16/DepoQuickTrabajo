using Backend.SQL;
using Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ServicioNotificacion
    {
        private readonly SqlRepositorioNotificacion _repositorioNotificacion;

        public ServicioNotificacion(SqlRepositorioNotificacion repositorioNotificacion)
        {
            _repositorioNotificacion = repositorioNotificacion;
        }

        public void GenerarNotificacion(Reserva reserva, string mensaje)
        {
            var notificacion = new Notificacion
            {
                Mensaje = mensaje,
                Usuario = reserva.Cliente,
                Reserva = reserva,
                Fecha = DateTime.Now
            };

            _repositorioNotificacion.AgregarNotificacion(notificacion);
        }

        public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int usuarioId)
        {
            var notificaciones = _repositorioNotificacion.ObtenerNotificacionesPorUsuario(usuarioId);
            return notificaciones.Select(MapearNotificacionADto).ToList();
        }

        public void EliminarNotificacion(int id)
        {
            _repositorioNotificacion.EliminarNotificacion(id);
        }

        private DtoNotificacion MapearNotificacionADto(Notificacion notificacion)
        {
            return new DtoNotificacion
            {
                ID = notificacion.ID,
                Mensaje = notificacion.Mensaje,
                Usuario = notificacion.Usuario,
                Reserva = notificacion.Reserva,
                Fecha = notificacion.Fecha
            };
        }
    }
}
