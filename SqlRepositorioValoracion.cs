using Backend.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.SQL
{
    public class SqlRepositorioValoracion
    {
        private readonly AppDataContext _database;
        private readonly SqlRepositorioRegistroAcciones _repositorioRegistro;

        public SqlRepositorioValoracion(AppDataContext database, SqlRepositorioRegistroAcciones repositorioRegistro)
        {
            _database = database;
            _repositorioRegistro = repositorioRegistro;
        }

        public void AgregarValoracion(Valoracion unaValoracion)
        {
            _repositorioRegistro.AgregarRegistroAccion("Creo una valoracion", unaValoracion.Usuario.Nombre, unaValoracion.Usuario.Apellido, DateTime.Now);
            _database.Valoraciones.Add(unaValoracion);
            _database.SaveChanges();
        }

        public bool ExisteValoracion(Valoracion unaValoracion)
        {
            return _database.Valoraciones.Any(v => v.ID == unaValoracion.ID);
        }

        public void BorrarValoracion(Valoracion unaValoracion)
        {
            _database.Valoraciones.Remove(unaValoracion);
            _database.SaveChanges();
        }

        public List<Valoracion> ObtenerValoraciones()
        {
            return _database.Valoraciones.ToList();
        }

        public Valoracion RetornarValoracionPorId(int id)
        {
            return _database.Valoraciones.FirstOrDefault(v => v.ID == id);
        }
    }
}
