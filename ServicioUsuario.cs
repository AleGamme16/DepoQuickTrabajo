using Backend.DTOs;
using Backend.SQL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Services
{
    public class ServicioUsuario
    {
        private readonly SqlRepositorioUsuario _sqlRepositorioUsuario;

        public ServicioUsuario(SqlRepositorioUsuario sqlRepositorioUsuario)
        {
            _sqlRepositorioUsuario = sqlRepositorioUsuario;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            _sqlRepositorioUsuario.AgregarUsuario(usuario);
        }

        public Usuario RetornarUsuarioPorId(int id)
        {
            return _sqlRepositorioUsuario.EncontrarUsuarioPorId(id);
        }

        public List<Usuario> TraerListaUsuarios()
        {
            return _sqlRepositorioUsuario.GetAll();
        }
        public List<Usuario> ObtenerClientes()
        {
            return _sqlRepositorioUsuario.GetAll().Where(u => u.Rol == EnumRol.Cliente).ToList();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            _sqlRepositorioUsuario.ActualizarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            _sqlRepositorioUsuario.EliminarUsuario(id);
        }

        public bool VerificarMail(string email)
        {
            string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return (Regex.IsMatch(email, regex) && email.Length < 100);
        }

        public bool VerificarContrasena(string contrasena)
        {
            bool numero = false;
            bool caracterEspecial = false;
            bool mayuscula = false;
            bool minuscula = false;

            char[] caracteresEspeciales = { '#', '@', '$', '.', ',', '%' };

            for (int i = 0; i < contrasena.Length; i++)
            {
                if (char.IsDigit(contrasena[i]))
                {
                    numero = true;
                }
                else if (char.IsLower(contrasena[i]))
                {
                    minuscula = true;
                }
                else if (char.IsUpper(contrasena[i]))
                {
                    mayuscula = true;
                }
                else if (caracteresEspeciales.Contains(contrasena[i]))
                {
                    caracterEspecial = true;
                }
            }

            return numero && caracterEspecial && mayuscula && minuscula && contrasena.Length >= 8;
        }

        public bool VerificarSegundaContra(string contrasena, string segContrasena)
        {
            if (contrasena.Length != segContrasena.Length)
            {
                return false;
            }

            for (int i = 0; i < contrasena.Length; i++)
            {
                if (contrasena[i] != segContrasena[i]) return false;
            }

            return true;
        }

        public bool ValidarNombreOApellido(string nombre)
        {
            if (nombre == null || !Regex.IsMatch(nombre, @"^[a-zA-Z](?!.*[@.])[a-zA-Z]+$"))
            {
                return false;
            }
            return !nombre.Contains("..") && !nombre.Contains(".") && !nombre.Contains(",");
        }

        public bool ExisteUsuarioConEseMailYContrasena(string mailUsuario, string password)
        {
            return _sqlRepositorioUsuario.ObtenerTodos().Any(u => u.Mail == mailUsuario && u.Contrasena == password);
        }

        public bool ExisteUsuarioConEseMail(string mail)
        {
            return _sqlRepositorioUsuario.ObtenerTodos().Any(u => u.Mail == mail);
        }

        public bool ExisteAdmin()
        {
            return _sqlRepositorioUsuario.ObtenerTodos().Any(u => u.Rol == EnumRol.Administrador);
        }

        public Usuario RetornarUsuarioPorMailYContrasena(string username, string password)
        {
            return _sqlRepositorioUsuario.ObtenerTodos().FirstOrDefault(u => u.Mail == username && u.Contrasena == password);
        }

        public void InicializarAdministrador(string nombre, string apellido, string password, string mail)
        {
            var admin = new Administrador
            {
                Nombre = nombre,
                Apellido = apellido,
                Contrasena = password,
                Mail = mail,
                Rol = EnumRol.Administrador
            };
            _sqlRepositorioUsuario.AgregarUsuario(admin);
        }

        public Usuario RetornarAdmin()
        {
            return _sqlRepositorioUsuario.GetAll().FirstOrDefault(u => u.Rol == EnumRol.Administrador);
        }

        public DtoUsuario RetornarUsuarioDtoPorMailYContrasena(string mail, string contrasena)
        {
            var usuario =RetornarUsuarioPorMailYContrasena(mail, contrasena);
            if (usuario == null)
            {
                return null;
            }

            return new DtoUsuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Contrasena = usuario.Contrasena,
                Mail = usuario.Mail,
                ID = usuario.ID,
                Rol = usuario.Rol
            };
        }

        public Cliente ObtenerClientePorId(int id)
        {
            return _sqlRepositorioUsuario.EncontrarClientePorId(id);
        }

    }
}
