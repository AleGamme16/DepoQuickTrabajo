using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Administrador : Usuario
    {


        public Administrador()
        {
        }
        public Administrador(string nombre, string apellido, string contrasena, string email)
            : base(nombre, apellido, contrasena, email)
        {

        }

    }

}
