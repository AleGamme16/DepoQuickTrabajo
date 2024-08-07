using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasena { get; set; }
        public string Mail { get; set; }
        public int ID { get; set; }

        [EnumDataType(typeof(EnumRol))]
        public EnumRol Rol { get; set; }


        public Usuario()
        {
        }

        public Usuario(string nombre, string apellido, string contrasena, string email) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Contrasena = contrasena;
            Mail = email;

            Rol = new EnumRol();
        }

        public override bool Equals(object obj)
        {
            // Verifica si el Usuario es nulo o no es del mismo tipo
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Convierte el objeto a Usuario para comparar las propiedades
            Usuario user = (Usuario)obj;

            // Compara las propiedades relevantes
            return Nombre == user.Nombre &&
                   Apellido == user.Apellido &&
                   Contrasena == user.Contrasena &&
                   Mail == user.Mail &&
                   ID == user.ID;
        }

        public override int GetHashCode()
        {
            // Se utiliza un algoritmo simple para generar un código hash único basado en las propiedades
            return HashCode.Combine(Nombre, Apellido, Contrasena, Mail, ID);
        }

    }

}
