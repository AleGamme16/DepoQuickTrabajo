using System.Collections.Generic;
using System.Linq;
using Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.SQL
{
    public class SqlRepositorioReserva
    {
        private readonly AppDataContext _context;

        public SqlRepositorioReserva(AppDataContext context)
        {
            _context = context;
        }

        public void AgregarReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public void AgregarDisponibilidad(Disponibilidad disponibilidad)
        {
            _context.Disponibilidades.Add(disponibilidad);
            _context.SaveChanges();
        }

        public List<Disponibilidad> ObtenerDisponibilidades(int depositoID)
        {
            return _context.Disponibilidades
                .Where(d => d.DepositoID == depositoID)
                .ToList();
        }

        public void BorrarReserva(Reserva reserva)
        {
            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
        }

        public bool ExisteReserva(Reserva unaReserva)
        {
            return _context.Reservas.Contains(unaReserva);
        }

        public Reserva BuscarReservaPorId(int id)
        {
            return _context.Reservas.Include(r => r.Cliente).Include(r => r.Deposito).FirstOrDefault(r => r.ID == id);
        }


        public Deposito RetornarDepositoPorReservaId(int reservaId)
        {
            var reserva = _context.Reservas.Include(r => r.Deposito).FirstOrDefault(r => r.ID == reservaId);
            return reserva?.Deposito;
        }

        public List<Deposito> ObtenerDepositosNoReservados()
        {
            var depositosReservadosIds = _context.Reservas.Select(r => r.Deposito.ID).ToList();
            return _context.Depositos
                .Where(d => !depositosReservadosIds.Contains(d.ID))
                .ToList();
        }

        public IQueryable<Reserva> ObtenerReservas()
        {
            return _context.Reservas.Include(r => r.Cliente).Include(r => r.Deposito).AsQueryable();
        }


        public void ActualizarReserva(Reserva unaReserva)
        {
            var reservaExistente = _context.Reservas.Find(unaReserva.ID);
            if (reservaExistente != null)
            {
                _context.Entry(reservaExistente).CurrentValues.SetValues(unaReserva);
                _context.SaveChanges();
            }
        }
    }
}

