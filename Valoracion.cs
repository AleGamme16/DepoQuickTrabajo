using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Valoracion
    {
        public int Estrellas { get; set; }
        public string Comentario { get; set; }
        public Deposito Deposito { get; set; }
        public Usuario Usuario { get; set; }
        public int ID { get; set; }

        public Valoracion()
        {
        }

        public Valoracion(int estrellas, string comentario, Deposito depo, Usuario user)
        {
            // Validaciones
            //validarEstrellas(estrellas);
            //validarComentario(comentario);

            Estrellas = estrellas;
            Comentario = comentario;
            Deposito = depo;
            Usuario = user;
        }

    }

}
