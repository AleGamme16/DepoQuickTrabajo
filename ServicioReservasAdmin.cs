using Backend.Context;
using Backend.Controllers;
using Backend.DTOs;
using Backend.SQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ServicioReservasAdmin
    {
        //private readonly SqlRepositorioReserva _sqlRepositorioReserva;
        //private readonly SqlRepositorioDeposito _sqlRepositorioDeposito;
        //private readonly SqlRepositorioUsuario _sqlRepositorioUsuario;
        //private readonly SqlRepositorioValoracion _sqlRepositorioValoracion;
        private readonly AppDataContext _context;

        public void RechazarSolicitud(Reserva unaReserva)
        {
            ValidarEstadoPendiente(unaReserva);
            ValidarQueHayaMotivoDeRechazo(unaReserva);
            ValidarLongitudDelMotivoDeRechazo(unaReserva);

            unaReserva.Estado = EnumEstado.Rechazada;
            _context.SaveChanges();
        }

        public bool ValidarEstadoPendiente(Reserva unaReserva)
        {
            if (unaReserva.Estado != EnumEstado.Pendiente)
            {
                return false;
            }
            return true;
        }

        public void ValidarQueHayaMotivoDeRechazo(Reserva unaReserva)
        {
            if (string.IsNullOrEmpty(unaReserva.MotivoRechazo))
            {
                throw new ArgumentNullException("Debe proporcionar un motivo de rechazo.");
            }
        }

        public void ValidarLongitudDelMotivoDeRechazo(Reserva unaReserva)
        {
            if (unaReserva.MotivoRechazo.Length > 300)
            {
                throw new ArgumentOutOfRangeException("El motivo de rechazo no debe superar los 300 caracteres.");
            }
        }

        public void AprobarSolicitudReserva(Reserva unaReserva)
        {
            if (ValidarEstadoPendiente(unaReserva)){

                unaReserva.Estado = EnumEstado.Aprobada;
                unaReserva.EstadoPago = EnumEstadoPago.Capturado;
                _context.SaveChanges();
            }
            //else throw new InvalidOperationException("La solicitud no está en estado pendiente.");
        }

        public bool EsUnaSolicitudPendiente(Reserva unaReserva)
        {
            return unaReserva.Estado == EnumEstado.Pendiente;
        }

        public bool ReservaEstaActiva(Reserva unaReserva)
        {

            return (unaReserva.Estado == EnumEstado.Pendiente || unaReserva.Estado == EnumEstado.Aprobada);
        }
    }
}
