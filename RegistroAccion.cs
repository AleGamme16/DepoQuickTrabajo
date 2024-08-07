using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class RegistroAccion
    {
        public int ID { get; set; }
        public string TipoAccion { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        //public Usuario Usuario { get; set; }
        public DateTime FechaHora { get; set; }
        

        public RegistroAccion() { }

        public RegistroAccion(string tipoAccion, string usuarioNombre, string usuarioApellido, DateTime fecha) { 
            TipoAccion = tipoAccion;
            UsuarioNombre = usuarioNombre;
            UsuarioApellido = usuarioApellido;
            FechaHora = fecha;
        }

    }
}
