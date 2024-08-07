using Backend.SQL;
using Backend.DTOs;
using System;

namespace Backend.Services
{
    public class ServicioSessionLogic
    {
        private readonly SqlRepositorioUsuario _repositorioUsuario;
        private readonly SqlRepositorioRegistroAcciones _repositorioRegistro;

        public Usuario UsuarioActual { get; private set; }
        public Cliente ClienteActual { get; private set; }

        public ServicioSessionLogic(SqlRepositorioUsuario repositorioUsuario, SqlRepositorioRegistroAcciones repositorioRegistro)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioRegistro = repositorioRegistro;
        }

        public void LoginCliente(DtoUsuario usuarioDto)
        {
            ClienteActual = new Cliente
            {
                ID = usuarioDto.ID,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                Contrasena = usuarioDto.Contrasena,
                Mail = usuarioDto.Mail,
                Rol = usuarioDto.Rol
            };

            if (ClienteActual != null && ClienteActual.Rol == EnumRol.Cliente)
            {
                _repositorioRegistro.AgregarRegistroAccion("Inicio sesión", ClienteActual.Nombre, ClienteActual.Apellido, DateTime.Now);
            }
        }


        public void LoginAdmin(DtoUsuario usuarioDto)
        {
            UsuarioActual = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                Contrasena = usuarioDto.Contrasena,
                Mail = usuarioDto.Mail,
                ID = usuarioDto.ID,
                Rol = usuarioDto.Rol
            };
        }

        public void Logout()
        {
            if (ClienteActual != null && ClienteActual.Rol == EnumRol.Cliente)
            {
                _repositorioRegistro.AgregarRegistroAccion("Cerró sesión", ClienteActual.Nombre, ClienteActual.Apellido, DateTime.Now);
            }

            UsuarioActual = null;
            ClienteActual = null;
        }
    }
}
