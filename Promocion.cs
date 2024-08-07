using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Promocion
    {
        public int ID;
        private DateTime desde;
        private DateTime hasta;

        public string Etiqueta { get; set; }
        public decimal Descuento { get; set; }

        public DateTime Desde
        {
            get => desde;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha actual.");
                }
                if (hasta != default(DateTime) && value > hasta)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
                }
                desde = value;
            }
        }

        public DateTime Hasta
        {
            get => hasta;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha actual.");
                }
                if (desde != default(DateTime) && value < desde)
                {
                    throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");
                }
                hasta = value;
            }
        }

        //public Promocion()
        //{
        //    DateTime desde = new DateTime(2024, 02, 03);
        //    DateTime hasta = new DateTime(2024, 04, 04);

        //    Etiqueta = "";
        //    Descuento = 0.10m;
        //    Desde = desde;
        //    Hasta = hasta;
        //    ID = 9999;
        //}

        public Promocion() { }

        public Promocion(string etiqueta, decimal descuento, DateTime desde, DateTime hasta)
        {
            Etiqueta = etiqueta;
            Descuento = descuento;
            Desde = desde;
            Hasta = hasta;
        }

        //public Promocion(DateTime desde, DateTime hasta)
        //{

        //    Etiqueta = "ParaPruebas";
        //    Descuento = 0.25m;
        //    Desde = desde;
        //    Hasta = hasta;

        //    ContadorDeID++;
        //    ID = ContadorDeID;
        //}
 

        public override bool Equals(object obj)
        {
            // Verifica si la Promo es nula o no es del mismo tipo
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Convertir el objeto a Promocion para comparar las propiedades
            Promocion promo = (Promocion)obj;

            // Compara las propiedades relevantes
            return Etiqueta == promo.Etiqueta &&
                   Descuento == promo.Descuento &&
                   Desde == promo.Desde &&
                   Hasta == promo.Hasta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Etiqueta, Descuento, Desde, Hasta);
        }

        public override string ToString()
        {
            return $"Promocion: {Etiqueta}, %: {Descuento}, Desde: {Desde}, Hasta: {Hasta}";
        }
    }

}
