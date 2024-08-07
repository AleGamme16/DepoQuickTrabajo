using Backend.DTOs;
using Backend.Services;
using System.Collections.Generic;

namespace Backend.Controllers
{
    public class ControllerPromocion
    {
        private readonly ServicioPromocion _servicioPromocion;

        public ControllerPromocion(ServicioPromocion servicioPromocion)
        {
            _servicioPromocion = servicioPromocion;
        }

        private Promocion MapearDtoPromocionAEntidad(DtoPromocion dtoPromocion)
        {
            return new Promocion
            {
                Etiqueta = dtoPromocion.Etiqueta,
                Descuento = dtoPromocion.Descuento,
                Desde = dtoPromocion.Desde,
                Hasta = dtoPromocion.Hasta,
                ID = dtoPromocion.ID
            };
        }

        public void AgregarPromocion(DtoPromocion dtoPromocion)
        {
            var promo = MapearDtoPromocionAEntidad(dtoPromocion);
            _servicioPromocion.AgregarPromocion(promo);
        }


        public List<DtoPromocion> TraerPromocionesDto()
        {
            List<Promocion> promociones = _servicioPromocion.TraerListaPromociones();
            List<DtoPromocion> dtoPromociones = new List<DtoPromocion>();

            foreach (var promocion in promociones)
            {
                dtoPromociones.Add(new DtoPromocion
                {
                    Etiqueta = promocion.Etiqueta,
                    Descuento = promocion.Descuento,
                    Desde = promocion.Desde,
                    Hasta = promocion.Hasta,
                    ID = promocion.ID
                });
            }

            return dtoPromociones;
        }

        public DtoPromocion EncontrarPromocionPorId(int id)
        {
            Promocion promocion = _servicioPromocion.EncontrarPromocionPorId(id);
            if (promocion != null)
            {
                return new DtoPromocion
                {
                    Etiqueta = promocion.Etiqueta,
                    Descuento = promocion.Descuento,
                    Desde = promocion.Desde,
                    Hasta = promocion.Hasta,
                    ID = promocion.ID
                };
            }
            return null;
        }

        public void ActualizarPromocion(DtoPromocion dtoPromocion)
        {
            Promocion promocion = MapearDtoPromocionAEntidad(dtoPromocion);
            _servicioPromocion.ActualizarPromocion(promocion);
        }

        public void EliminarPromocion(int id)
        {
            _servicioPromocion.EliminarPromocion(id);
        }
    }
}
