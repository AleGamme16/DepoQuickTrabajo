using Backend.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.SQL
{
    public class SqlRepositorioNotificacion
    {
        private readonly AppDataContext _context;

        public SqlRepositorioNotificacion(AppDataContext context)
        {
            _context = context;
        }

        public void AgregarNotificacion(Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            _context.SaveChanges();
        }

        public List<Notificacion> ObtenerNotificacionesPorUsuario(int usuarioId)
        {
            return _context.Notificaciones.Where(n => n.Usuario.ID == usuarioId).ToList();
        }

        public void EliminarNotificacion(int id)
        {
            var notificacion = _context.Notificaciones.Find(id);
            if (notificacion != null)
            {
                _context.Notificaciones.Remove(notificacion);
                _context.SaveChanges();
            }
        }
    }
}
