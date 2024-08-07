using Backend.Context;
using Backend;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend.SQL
{
    public class SqlRepositorioPromocion
    {
        private readonly AppDataContext _database;

        public SqlRepositorioPromocion(AppDataContext database)
        {
            _database = database;
        }

        public void AgregarPromocion(Promocion unaPromocion)
        {
            _database.Promociones.Add(unaPromocion);
            _database.SaveChanges();
        }

        public bool ExistePromocion(Promocion unaPromocion)
        {
            return _database.Promociones.Any(p => p.ID == unaPromocion.ID);
        }

        public void BorrarPromocion(Promocion unaPromocion)
        {
            _database.Promociones.Remove(unaPromocion);
            _database.SaveChanges();
        }

        public List<Promocion> ObtenerPromociones()
        {
            return _database.Promociones.ToList();
        }

        public Promocion RetornarPromocionPorId(int id)
        {
            return _database.Promociones.FirstOrDefault(p => p.ID == id);
        }

        public void ActualizarPromocion(Promocion promocion)
        {
            var existingPromocion = _database.Promociones.Find(promocion.ID);
            if (existingPromocion != null)
            {
                _database.Entry(existingPromocion).CurrentValues.SetValues(promocion);
                _database.SaveChanges();
            }
        }
    }
}
