using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Backend
{
    public class Deposito
    {
        public EnumArea Area { get; set; }
        public EnumTamano Tamano { get; set; }
        public bool Climatizado { get; set; }
        public Promocion Promo { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public ICollection<Disponibilidad> Disponibilidades { get; set; }=new List<Disponibilidad>();

        public static int ContadorDeID { get; set; } = 0;

        //public Deposito()
        //{
        //    Area = EnumArea.A;
        //    Tamano = EnumTamano.Pequeno;
        //    Climatizado = false;
        //    Promo = new Promocion();
        //    ID = 9999;
        //}
        public Deposito() { }

        public Deposito(EnumArea area, EnumTamano tamano, bool climatizado, Promocion promo)
        {
            Area = area;
            Tamano = tamano;
            Climatizado = climatizado;
            Promo = promo;
            Disponibilidades= new List<Disponibilidad>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Deposito other = (Deposito)obj;

            return string.Equals(Area, other.Area) &&
                   string.Equals(Tamano, other.Tamano) &&
                   Climatizado == other.Climatizado &&
                   (Promo != null ? Promo.ID == other.Promo?.ID : other.Promo == null) &&
                   ID == other.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Area, Tamano, Climatizado, Promo?.ID, ID);
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}  ,Area: {Area},  Tamaño: {Tamano},  Climatizacion: {(Climatizado ? "Con climatizacion" : "Sin climatizacion")}";
        }
    }
}
