using Backend.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.SQL
{
    public class SqlRepositorioRegistroAcciones
    {
        private readonly AppDataContext _database;

        public SqlRepositorioRegistroAcciones(AppDataContext database)
        {
            _database = database;
        }

        public void AgregarRegistroAccion(string tipoAccion, string usuarioNombre, string usuarioApellido, DateTime fecha)
        {
            if (string.IsNullOrEmpty(usuarioNombre) || string.IsNullOrEmpty(usuarioApellido))
                throw new ArgumentNullException("El nombre y apellido del usuario no pueden ser nulos o vacíos.");

            // Verificar si el usuario ya está siendo rastreado por el contexto
            var usuarioExistente = _database.Usuarios.SingleOrDefault(u => u.Nombre == usuarioNombre && u.Apellido == usuarioApellido);
            if (usuarioExistente == null)
            {
                throw new InvalidOperationException("El usuario especificado no existe en la base de datos.");
            }

            RegistroAccion registro = new RegistroAccion
            {
                TipoAccion = tipoAccion,
                UsuarioNombre = usuarioNombre,
                UsuarioApellido = usuarioApellido,
                FechaHora = fecha
            };

            _database.Registros.Add(registro);
            _database.SaveChanges();
        }




        public List<RegistroAccion> ObtenerTodosLosRegistros()
        {
            return _database.Registros.ToList();
        }
    }
}
