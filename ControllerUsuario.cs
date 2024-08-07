using Backend.DTOs;
using Backend.Services;
using Backend;

namespace Backend.Controllers
{
    public class ControllerUsuario
    {
        private readonly ServicioUsuario _servicioUsuario;

        public ControllerUsuario(ServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        public Usuario MapearDtoUsuarioAEntidad(DtoUsuario dtoUsuario)
        {
            return new Usuario
            {
                Nombre = dtoUsuario.Nombre,
                Apellido = dtoUsuario.Apellido,
                Contrasena = dtoUsuario.Contrasena,
                Mail = dtoUsuario.Mail,
                ID = dtoUsuario.ID,
                Rol = dtoUsuario.Rol
            };
        }

        public DtoUsuario MapearEntidadUsuarioADto(Usuario usuario)
        {
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

        public void AgregarUsuario(DtoUsuario dtoUsuario)
        {
            Cliente usuario = MapearDtoUsuarioACliente(dtoUsuario);
            _servicioUsuario.AgregarUsuario(usuario);
        }

        public List<DtoUsuario> TraerUsuarios()
        {
            List<Usuario> usuarios = _servicioUsuario.TraerListaUsuarios();
            List<DtoUsuario> dtoUsuarios = new List<DtoUsuario>();

            foreach (var usuario in usuarios)
            {
                dtoUsuarios.Add(MapearEntidadUsuarioADto(usuario));
            }

            return dtoUsuarios;
        }

        public DtoUsuario EncontrarUsuarioPorId(int id)
        {
            Usuario usuario = _servicioUsuario.RetornarUsuarioPorId(id);
            return usuario != null ? MapearEntidadUsuarioADto(usuario) : null;
        }

        public void ActualizarUsuario(DtoUsuario dtoUsuario)
        {
            Usuario usuario = MapearDtoUsuarioAEntidad(dtoUsuario);
            _servicioUsuario.ActualizarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            _servicioUsuario.EliminarUsuario(id);
        }

        public void InicializarAdministrador(DtoUsuario dtoUsuario)
        {
            _servicioUsuario.InicializarAdministrador(dtoUsuario.Nombre, dtoUsuario.Apellido, dtoUsuario.Contrasena, dtoUsuario.Mail);
        }

        public DtoUsuario RetornarAdmin()
        {
            var admin = _servicioUsuario.RetornarAdmin();
            return admin != null ? MapearEntidadUsuarioADto(admin) : null;
        }

        public bool ExisteUsuarioConEseMailYContrasena(string mail, string contrasena)
        {
            return _servicioUsuario.ExisteUsuarioConEseMailYContrasena(mail, contrasena);
        }

        public DtoUsuario RetornarUsuarioPorMailYContrasena(string mail, string contrasena)
        {
            var usuario = _servicioUsuario.RetornarUsuarioPorMailYContrasena(mail, contrasena);
            if (usuario == null)
                return null;

            return new DtoUsuario
            {
                ID = usuario.ID,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Contrasena = usuario.Contrasena,
                Mail = usuario.Mail,
                Rol = usuario.Rol
            };
        }

        public Cliente MapearDtoUsuarioACliente(DtoUsuario dtoUsuario)
        {
            return new Cliente
            {
                Nombre = dtoUsuario.Nombre,
                Apellido = dtoUsuario.Apellido,
                Contrasena = dtoUsuario.Contrasena,
                Mail = dtoUsuario.Mail,
                ID = dtoUsuario.ID,
                Rol = dtoUsuario.Rol
            };
        }

        public Cliente RetornarClientePorMailYContrasena(string mail, string contrasena)
        {
            var usuarioDto = new DtoUsuario();
            if (_servicioUsuario.ExisteUsuarioConEseMailYContrasena(mail, contrasena))
            {
                usuarioDto = _servicioUsuario.RetornarUsuarioDtoPorMailYContrasena(mail, contrasena);
                return MapearDtoUsuarioACliente(usuarioDto);
            }
            return null;
        }
    }
}


