using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Cliente : Usuario
    {

        public Cliente(string nombre, string apellido, string contrasena, string email)
            : base(nombre, apellido, contrasena, email)
        {

        }
        //public Cliente()
        //{
        //    Nombre = "Iker";
        //    Apellido = "Casillas";
        //    Contrasena = "campeones2010";
        //    Mail = "iker@gmail.com";

        //    ID = 99998;
        //}

        public Cliente() { }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellido: {Apellido}, Mail: {Mail}";
        }

    }

}
