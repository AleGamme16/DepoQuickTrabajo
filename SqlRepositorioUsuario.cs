using Backend.Context;
using System.Linq;

namespace Backend.SQL
{
    public class SqlRepositorioUsuario
    {
        private readonly AppDataContext _context;

        public SqlRepositorioUsuario(AppDataContext context)
        {
            _context = context;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario EncontrarUsuarioPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.ID == id);
        }

        public Cliente EncontrarClientePorId(int id)
        {
            return _context.Usuarios.OfType<Cliente>().FirstOrDefault(c => c.ID == id);
        }

        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public IQueryable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.AsQueryable();
        }
    }
}


