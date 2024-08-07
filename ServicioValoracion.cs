using Backend.SQL;
using Backend.DTOs;
using System;
using System.Collections.Generic;

namespace Backend.Services
{
    public class ServicioValoracion
    {
        private readonly SqlRepositorioValoracion _sqlRepositorioValoracion;
        private readonly SqlRepositorioUsuario _sqlRepositorioUsuario;
        private readonly SqlRepositorioDeposito _sqlRepositorioDeposito;
        private readonly SqlRepositorioRegistroAcciones _repositorioRegistro;

        public ServicioValoracion(SqlRepositorioValoracion sqlRepositorioValoracion, SqlRepositorioUsuario sqlRepositorioUsuario,
                                  SqlRepositorioDeposito sqlRepositorioDeposito, SqlRepositorioRegistroAcciones repositorioRegistro)
        {
            _sqlRepositorioValoracion = sqlRepositorioValoracion;
            _sqlRepositorioUsuario = sqlRepositorioUsuario;
            _sqlRepositorioDeposito = sqlRepositorioDeposito;
            _repositorioRegistro = repositorioRegistro;
        }

        public void AgregarValoracion(DtoValoracion dtoValoracion)
        {
            var usuario = _sqlRepositorioUsuario.EncontrarUsuarioPorId(dtoValoracion.UsuarioID);
            var deposito = _sqlRepositorioDeposito.RetornarDepositoPorId(dtoValoracion.DepositoID);

            if (usuario == null || deposito == null)
            {
                throw new ArgumentNullException("Usuario o depósito no encontrados.");
            }

            var valoracion = new Valoracion
            {
                Estrellas = dtoValoracion.Estrellas,
                Comentario = dtoValoracion.Comentario,
                Usuario = usuario,
                Deposito = deposito
            };

            _sqlRepositorioValoracion.AgregarValoracion(valoracion);
            _repositorioRegistro.AgregarRegistroAccion("Creó una valoración", usuario.Nombre, usuario.Apellido, DateTime.Now);
        }

        public List<DtoValoracion> ObtenerValoraciones()
        {
            var valoraciones = _sqlRepositorioValoracion.ObtenerValoraciones();
            var dtoValoraciones = new List<DtoValoracion>();

            foreach (var valoracion in valoraciones)
            {
                dtoValoraciones.Add(new DtoValoracion
                {
                    UsuarioID = valoracion.Usuario.ID,
                    DepositoID = valoracion.Deposito.ID,
                    Estrellas = valoracion.Estrellas,
                    Comentario = valoracion.Comentario,
                    ValoracionID = valoracion.ID
                });
            }

            return dtoValoraciones;
        }
    }
}


