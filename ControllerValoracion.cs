using Backend.DTOs;
using Backend.Services;
using System.Collections.Generic;

namespace Backend.Controllers
{
    public class ControllerValoracion
    {
        private readonly ServicioValoracion _servicioValoracion;

        public ControllerValoracion(ServicioValoracion servicioValoracion)
        {
            _servicioValoracion = servicioValoracion;
        }

        public void AgregarValoracion(DtoValoracion dtoValoracion)
        {
            _servicioValoracion.AgregarValoracion(dtoValoracion);
        }

        public List<DtoValoracion> ObtenerValoraciones()
        {
            return _servicioValoracion.ObtenerValoraciones();
        }
    }
}
