using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Services;

namespace Backend.Controllers
{
    public class ControllerNotificacion
    {
        private readonly ServicioNotificacion _servicioNotificacion;

        public ControllerNotificacion(ServicioNotificacion servicioNotificacion)
        {
            _servicioNotificacion = servicioNotificacion;
        }

        public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int usuarioId)
        {
            return _servicioNotificacion.ObtenerNotificacionesPorUsuario(usuarioId);
        }

        public void EliminarNotificacion(int id)
        {
            _servicioNotificacion.EliminarNotificacion(id);
        }
    }
}
