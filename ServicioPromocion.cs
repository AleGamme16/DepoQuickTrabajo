using Backend.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ServicioPromocion
    {
        private readonly SqlRepositorioPromocion _sqlRepositorioPromocion;

        public ServicioPromocion(SqlRepositorioPromocion sqlRepositorioPromocion)
        {
            _sqlRepositorioPromocion = sqlRepositorioPromocion;
        }

        public void AgregarPromocion(Promocion promocion)
        {
            if (promocion.Desde > promocion.Hasta)
            {
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha final.");
            }
            if (promocion.Desde < DateTime.Today)
            {
                throw new ArgumentException("La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            _sqlRepositorioPromocion.AgregarPromocion(promocion);
        }


        public Promocion EncontrarPromocionPorId(int id)
        {
            return _sqlRepositorioPromocion.RetornarPromocionPorId(id);
        }

        public List<Promocion> TraerListaPromociones()
        {
            return _sqlRepositorioPromocion.ObtenerPromociones();
        }

        public void ActualizarPromocion(Promocion promocion)
        {
            _sqlRepositorioPromocion.ActualizarPromocion(promocion);
        }

        public void EliminarPromocion(int id)
        {
            Promocion promo = EncontrarPromocionPorId(id);
            _sqlRepositorioPromocion.BorrarPromocion(promo);
        }

        //public void validarEtiqueta(string etiqueta)
        //{
        //    if (etiqueta.Length > 20)
        //    {
        //        throw new ArgumentException("La etiqueta no puede tener mas de 20 caracteres.");
        //    }
        //}

        //public void validarDescuento(decimal descuento)
        //{
        //    if (descuento < 0.05m || descuento > 0.75m)
        //    {
        //        throw new ArgumentException("La promocion no puede ser menor a 5% ni mayor a 75%.");
        //    }
        //}

        //public void validarFechas(DateTime desde, DateTime hasta)
        //{
        //    if (desde <= DateTime.Today)
        //    {
        //        throw new ArgumentException("La promocion no puede empezar antes de mañana.");
        //    }
        //    if (hasta <= DateTime.Today)
        //    {
        //        throw new ArgumentException("La promocion no puede terminar antes de mañana.");
        //    }
        //    if (hasta <= desde)
        //    {
        //        throw new ArgumentException("La fecha de fin no puede ser menor o igual a la de inicio.");
        //    }
        //}
    }
}
