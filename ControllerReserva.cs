using Backend.DTOs;
using Backend.Services;
using Backend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    public class ControllerReserva
    {
        private ServicioReserva _servicioReserva;
        private readonly ServicioDeposito _servicioDeposito;
        private readonly ServicioUsuario _servicioUsuario;

        public ControllerReserva(ServicioReserva servicioReserva)
        {
            _servicioReserva = servicioReserva;
        }


        private Reserva MapearDtoReservaAEntidad(DtoReserva unDtoReserva)
        {
            Reserva nuevaReserva = new Reserva
            {
                Cliente = (Cliente)_servicioReserva.BuscarUsuario(unDtoReserva.ClienteID),
                Deposito = _servicioReserva.RetornarDepositoPorId(unDtoReserva.DepositoID),
                FechaInicio = unDtoReserva.FechaInicio,
                FechaFin = unDtoReserva.FechaFin,
                Costo = unDtoReserva.Costo,
                Estado = EnumEstado.Pendiente,
                EstadoPago=EnumEstadoPago.Abonar,
            };
            return nuevaReserva;
        }

        public DtoReserva MapearEntidadADtoReserva(Reserva reserva)
        {
            if (reserva == null)
                throw new ArgumentNullException(nameof(reserva));

            if (reserva.Cliente == null)
                throw new InvalidOperationException("La reserva no tiene un cliente asociado.");

            if (reserva.Deposito == null)
                throw new InvalidOperationException("La reserva no tiene un depósito asociado.");

            return new DtoReserva
            {
                ID = reserva.ID,
                ClienteID = reserva.Cliente.ID,
                NombreCliente = reserva.Cliente.Nombre,
                ApellidoCliente = reserva.Cliente.Apellido,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                Costo = reserva.Costo,
                DepositoID = reserva.Deposito.ID,
                TamanoDeposito = reserva.Deposito.Tamano,
                MotivoRechazo = reserva.MotivoRechazo,
                Estado = reserva.Estado,
                EstadoPago = reserva.EstadoPago
            };
        }


        private Valoracion MapearDtoValoracionAEntidad(DtoValoracion unDtoValoracion)
        {
            Valoracion nuevaValoracion = new Valoracion
            {
                Usuario = _servicioReserva.BuscarUsuario(unDtoValoracion.UsuarioID),
                Deposito = _servicioReserva.RetornarDepositoPorId(unDtoValoracion.DepositoID),
                Comentario = unDtoValoracion.Comentario,
                Estrellas = unDtoValoracion.Estrellas
            };
            return nuevaValoracion;
        }

        public DtoValoracion MapearEntidadADtoValoracion(Valoracion valoracion)
        {
            return new DtoValoracion
            {
                UsuarioID = valoracion.Usuario.ID,
                DepositoID = valoracion.Deposito.ID,
                Comentario = valoracion.Comentario,
                Estrellas = valoracion.Estrellas
            };
        }

        public DtoDeposito MapearEntidadADtoDeposito(Deposito deposito)
        {
            return new DtoDeposito
            {
                ID = deposito.ID,
                Area = deposito.Area,
                Tamano = deposito.Tamano,
                Climatizado = deposito.Climatizado,
                PromocionId = deposito.Promo?.ID
            };
        }

        public List<DtoDeposito> TraerDepositosDto()
        {
            var depositos = _servicioReserva.TraerListaDepositos();
            return depositos.Select(MapearEntidadADtoDeposito).ToList();
        }

        public List<DtoValoracion> TraerValoracionesDto()
        {
            var valoraciones = _servicioReserva.TraerListaValoraciones();
            return valoraciones.Select(MapearEntidadADtoValoracion).ToList();
        }

        public List<DtoReserva> TraerReservasDto()
        {
            var reservas = _servicioReserva.TraerListaReservas()
                                           .Include(r => r.Cliente)
                                           .Include(r => r.Deposito)
                                           .ToList();

            return reservas.Select(MapearEntidadADtoReserva).ToList();
        }

        public void AgregarReserva(DtoReserva dtoReserva)
        {
            var reserva = MapearDtoReservaAEntidad(dtoReserva);
            if (!_servicioReserva.AgregarReserva(reserva))
            {
                throw new Exception("El depósito no está disponible en las fechas seleccionadas.");
            }
        }

        public void AgregarValoracion(DtoValoracion unDtoValoracion)
        {
            Valoracion entidadValoracion = MapearDtoValoracionAEntidad(unDtoValoracion);
            _servicioReserva.AgregarValoracion(entidadValoracion);
        }

        public decimal CalcularCostoReserva(DateTime fechaInicio, DateTime fechaFin, int depositoID)
        {
            Deposito depositoSeleccionado = _servicioReserva.RetornarDepositoPorId(depositoID);
            return _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, depositoSeleccionado);
        }

        public bool ReservaEstaRechazada(DtoReserva dtoReserva)
        {
            return dtoReserva.Estado == EnumEstado.Rechazada;
        }

        public List<DtoDeposito> ObtenerDepositosNoReservados()
        {
            var depositos = _servicioReserva.ObtenerDepositosNoReservados();
            return depositos.Select(MapearEntidadADtoDeposito).ToList();
        }

        public void AprobarSolicitud(DtoReserva dtoReserva)
        {
            var entidadReserva = _servicioReserva.BuscarReservaPorId(dtoReserva.ID);
            if (entidadReserva == null)
            {
                throw new ArgumentNullException(nameof(entidadReserva), "No se encontró la reserva especificada.");
            }
            _servicioReserva.AprobarSolicitud(entidadReserva);
        }

        public void RechazarSolicitud(DtoReserva dtoReserva, string motivoRechazo)
        {
            var entidadReserva = _servicioReserva.BuscarReservaPorId(dtoReserva.ID);
            if (entidadReserva == null)
            {
                throw new ArgumentNullException(nameof(entidadReserva), "No se encontró la reserva especificada.");
            }
            _servicioReserva.RechazarSolicitud(entidadReserva, motivoRechazo);
        }

        public void ProcesarPago(int reservaId)
        {
            _servicioReserva.ProcesarPago(reservaId);
        }

        public (byte[] content, string contentType, string fileName) ExportarReporte(string formato)
        {
            byte[] contenido;
            string tipoContenido;
            string nombreArchivo;

            switch (formato.ToLower())
            {
                case "csv":
                    contenido = _servicioReserva.GenerarReporteCSV();
                    tipoContenido = "text/csv";
                    nombreArchivo = "reporte_reservas.csv";
                    break;
                case "txt":
                    contenido = _servicioReserva.GenerarReporteTXT();
                    tipoContenido = "text/plain";
                    nombreArchivo = "reporte_reservas.txt";
                    break;
                default:
                    throw new ArgumentException("Formato no soportado");
            }

            return (contenido, tipoContenido, nombreArchivo);
        }
    }
}
