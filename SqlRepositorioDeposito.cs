using Backend.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.SQL
{
    public class SqlRepositorioDeposito
    {
        private readonly AppDataContext _database;

        public SqlRepositorioDeposito(AppDataContext database)
        {
            _database = database;
        }

        public void AgregarDeposito(Deposito deposito)
        {
            _database.Depositos.Add(deposito);
            _database.SaveChanges();
        }

        public void ActualizarDeposito(Deposito deposito)
        {
            var depositoExistente = _database.Depositos.Include(d => d.Disponibilidades).FirstOrDefault(d => d.ID == deposito.ID);
            if (depositoExistente != null)
            {
                _database.Entry(depositoExistente).CurrentValues.SetValues(deposito);
                _database.SaveChanges();
            }
        }
        public void BorrarDeposito(Deposito deposito)
        {
            _database.Depositos.Remove(deposito);
            _database.SaveChanges();
        }

        public bool ExisteDeposito(Deposito unDeposito)
        {
            return _database.Depositos.Any(d => d.ID == unDeposito.ID);
        }

        public List<Deposito> ObtenerDepositos()
        {
            return _database.Depositos.ToList();
        }

        public Deposito RetornarDepositoPorId(int id)
        {
            return _database.Depositos.FirstOrDefault(d => d.ID == id);
        }

        public bool EstaDisponible(int depositoID, DateTime fechaInicio, DateTime fechaFin)
        {
            var disponibilidades = _database.Disponibilidades
                .Where(d => d.DepositoID == depositoID &&
                            fechaInicio >= d.FechaInicio &&
                            fechaFin <= d.FechaFin)
                .ToList();

            return disponibilidades.Any();
        }

        public List<Deposito> ObtenerDepositosDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            var depositosDisponiblesIds = _database.Disponibilidades
                .Where(d => fechaInicio >= d.FechaInicio && fechaFin <= d.FechaFin)
                .Select(d => d.DepositoID)
                .Distinct()
                .ToList();

            return _database.Depositos
                .Where(d => depositosDisponiblesIds.Contains(d.ID))
                .ToList();
        }

        public void AgregarDisponibilidad(Disponibilidad disponibilidad)
        {
            _database.Disponibilidades.Add(disponibilidad);
            _database.SaveChanges();
        }

        public bool HayReservaEnFecha(int depositoID, DateTime fechaInicio, DateTime fechaFin)
        {
            var disponibilidades = _database.Disponibilidades
                .Where(d => d.DepositoID == depositoID)
                .ToList();

            foreach (var disponibilidad in disponibilidades)
            {
                if ((fechaInicio >= disponibilidad.FechaInicio && fechaInicio <= disponibilidad.FechaFin) ||
                    (fechaFin >= disponibilidad.FechaInicio && fechaFin <= disponibilidad.FechaFin) ||
                    (fechaInicio <= disponibilidad.FechaInicio && fechaFin >= disponibilidad.FechaFin))
                {
                    return true; // Las fechas se solapan
                }
            }

            return false; // No hay solapamiento de fechas
        }

        public void EliminarDisponibilidad(Disponibilidad disponibilidad)
        {
            _database.Disponibilidades.Remove(disponibilidad);
            _database.SaveChanges();
        }

        public List<Disponibilidad> ObtenerDisponibilidadesPorDepositoId(int depositoID)
        {
            return _database.Disponibilidades
                .Where(d => d.DepositoID == depositoID)
                .ToList();
        }

        public void ActualizarDisponibilidad(Disponibilidad disponibilidad)
        {
            _database.Disponibilidades.Update(disponibilidad);
            _database.SaveChanges();
        }
    }
}
