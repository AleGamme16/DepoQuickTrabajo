using Backend.DTOs;
using Backend.Services;
using System.Collections.Generic;

namespace Backend.Controllers
{
    public class ControllerRegistroAccion
    {
        private readonly ServicioRegistroAccion _servicioRegistroAccion;

        public ControllerRegistroAccion(ServicioRegistroAccion servicioRegistroAccion)
        {
            _servicioRegistroAccion = servicioRegistroAccion;
        }

        public void AgregarRegistroAccion(DtoRegistroAccion dtoRegistroAccion)
        {
            _servicioRegistroAccion.AgregarRegistroAccion(dtoRegistroAccion);
        }

        public List<DtoRegistroAccion> TraerRegistrosAccion()
        {
            return _servicioRegistroAccion.ObtenerTodosLosRegistros();
        }
    }
}
