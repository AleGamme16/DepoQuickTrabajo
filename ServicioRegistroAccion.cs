using Backend.DTOs;
using Backend.SQL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class ServicioRegistroAccion
    {
        private readonly SqlRepositorioRegistroAcciones _sqlRepositorioRegistroAcciones;

        public ServicioRegistroAccion(SqlRepositorioRegistroAcciones sqlRepositorioRegistroAcciones)
        {
            _sqlRepositorioRegistroAcciones = sqlRepositorioRegistroAcciones;
        }

        public void AgregarRegistroAccion(DtoRegistroAccion dtoRegistroAccion)
        {
            var registroAccion = new RegistroAccion
            {
                TipoAccion = dtoRegistroAccion.TipoAccion,
                UsuarioNombre = dtoRegistroAccion.UsuarioNombre,
                UsuarioApellido = dtoRegistroAccion.UsuarioApellido,
                FechaHora = dtoRegistroAccion.FechaHora
            };
            _sqlRepositorioRegistroAcciones.AgregarRegistroAccion(registroAccion.TipoAccion, registroAccion.UsuarioNombre, registroAccion.UsuarioApellido, registroAccion.FechaHora);
        }

        public List<DtoRegistroAccion> ObtenerTodosLosRegistros()
        {
            return _sqlRepositorioRegistroAcciones.ObtenerTodosLosRegistros()
                .Select(r => new DtoRegistroAccion
                {
                    ID = r.ID,
                    TipoAccion = r.TipoAccion,
                    UsuarioNombre = r.UsuarioNombre,
                    UsuarioApellido = r.UsuarioApellido,
                    FechaHora = r.FechaHora
                }).ToList();
        }
    }
}
