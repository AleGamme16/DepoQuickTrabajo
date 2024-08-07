using Backend.SQL;
using Backend;
using Backend.ManejoDeRepositorios;
using System.Text;

namespace Backend.Services
{
    public class ServicioReserva
    {
        private readonly SqlRepositorioReserva _sqlRepositorioReserva;
        private readonly SqlRepositorioDeposito _sqlRepositorioDeposito;
        private readonly SqlRepositorioUsuario _sqlRepositorioUsuario;
        private readonly SqlRepositorioValoracion _sqlRepositorioValoracion;
        private readonly ServicioNotificacion _servicioNotificacion;

        public ServicioReserva(SqlRepositorioReserva sqlRepositorioReserva, SqlRepositorioDeposito sqlRepositorioDeposito, SqlRepositorioUsuario sqlRepositorioUsuario, SqlRepositorioValoracion sqlRepositorioValoracion, ServicioNotificacion servicioNotificacion)
        {
            _sqlRepositorioReserva = sqlRepositorioReserva;
            _sqlRepositorioDeposito = sqlRepositorioDeposito;
            _sqlRepositorioUsuario = sqlRepositorioUsuario;
            _sqlRepositorioValoracion = sqlRepositorioValoracion;
            _servicioNotificacion = servicioNotificacion;
        }

        public bool AgregarReserva(Reserva reserva)
        {
            
            var disponibilidades = _sqlRepositorioReserva.ObtenerDisponibilidades(reserva.Deposito.ID);

            if (disponibilidades.Count > 0 && HayReservaEnFecha(disponibilidades, reserva.FechaInicio, reserva.FechaFin))
            {
                return false; 
            }

            _sqlRepositorioReserva.AgregarReserva(reserva);

            var nuevaDisponibilidad = new Disponibilidad
            {
                DepositoID = reserva.Deposito.ID,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin
            };
            _sqlRepositorioReserva.AgregarDisponibilidad(nuevaDisponibilidad);

            return true;
        }

        private bool HayReservaEnFecha(List<Disponibilidad> disponibilidades, DateTime fechaInicio, DateTime fechaFin)
        {
            foreach (var disponibilidad in disponibilidades)
            {
                if ((fechaInicio >= disponibilidad.FechaInicio && fechaInicio <= disponibilidad.FechaFin) ||
                    (fechaFin >= disponibilidad.FechaInicio && fechaFin <= disponibilidad.FechaFin) ||
                    (fechaInicio <= disponibilidad.FechaInicio && fechaFin >= disponibilidad.FechaFin))
                {
                    return true; // Las fechas se solapan, el depósito no está disponible
                }
            }

            return false; // No hay solapamiento de fechas, el depósito está disponible
        }

        public void AgregarValoracion(Valoracion unaValoracion)
        {
            _sqlRepositorioValoracion.AgregarValoracion(unaValoracion);
        }

        public Deposito RetornarDepositoPorId(int id)
        {
            return _sqlRepositorioDeposito.RetornarDepositoPorId(id);
        }

        public Usuario BuscarUsuario(int ID)
        {
            return _sqlRepositorioUsuario.EncontrarUsuarioPorId(ID);
        }

        public Reserva BuscarReservaPorId(int id)
        {
            var reserva = _sqlRepositorioReserva.BuscarReservaPorId(id);
            if (reserva == null)
            {
                throw new ArgumentNullException(nameof(reserva), "No se encontró la reserva especificada.");
            }
            return reserva;
        }


        public IQueryable<Reserva> TraerListaReservas()
        {
            return _sqlRepositorioReserva.ObtenerReservas();
        }

        public List<Deposito> TraerListaDepositos()
        {
            return _sqlRepositorioDeposito.ObtenerDepositos();
        }

        public List<Valoracion> TraerListaValoraciones()
        {
            return _sqlRepositorioValoracion.ObtenerValoraciones();
        }

        public decimal CalcularPrecioDeposito(DateTime fechaInicio, DateTime fechaFin, Deposito deposito)
        {
            decimal precioTotal = 0;
            decimal descuento = 0;

            int diasDeReserva = (int)Math.Ceiling((fechaFin - fechaInicio).TotalDays);

            switch (deposito.Tamano)
            {
                case EnumTamano.Pequeno:
                    precioTotal = diasDeReserva * 50;
                    break;
                case EnumTamano.Mediano:
                    precioTotal = diasDeReserva * 75;
                    break;
                case EnumTamano.Grande:
                    precioTotal = diasDeReserva * 100;
                    break;
                default:
                    break;
            }

            if (deposito.Climatizado)
            {
                precioTotal += 20 * diasDeReserva;
            }

            if (diasDeReserva > 6 && diasDeReserva < 15)
            {
                descuento += 0.05m;
            }
            else if (diasDeReserva > 13)
            {
                descuento += 0.1m;
            }

            if (deposito.Promo != null && deposito.Promo.Desde <= DateTime.Now &&
                deposito.Promo.Hasta >= DateTime.Now)
            {
                descuento += deposito.Promo.Descuento;
            }

            if (descuento <= 1.00m)
            {
                precioTotal *= (1 - descuento);
            }
            else
            {
                precioTotal = 0;
            }

            return precioTotal;
        }

        public List<Deposito> ObtenerDepositosNoReservados()
        {
            return _sqlRepositorioReserva.ObtenerDepositosNoReservados();
        }

        public void RechazarSolicitud(Reserva reserva, string motivoRechazo)
        {
            if (reserva == null)
            {
                throw new ArgumentNullException(nameof(reserva), "La reserva no puede ser nula.");
            }

            if (reserva.Estado != EnumEstado.Pendiente)
            {
                throw new InvalidOperationException("Solo se pueden rechazar reservas en estado pendiente.");
            }

            if (string.IsNullOrEmpty(motivoRechazo))
            {
                throw new InvalidOperationException("Debe proporcionar un motivo de rechazo.");
            }

            reserva.Estado = EnumEstado.Rechazada;
            reserva.EstadoPago = null;
            reserva.MotivoRechazo = motivoRechazo;
            _sqlRepositorioReserva.ActualizarReserva(reserva);

            var mensaje = $"Su reserva del depósito {reserva.Deposito.Nombre} ha sido rechazada. Motivo: {motivoRechazo}.";
            _servicioNotificacion.GenerarNotificacion(reserva, mensaje);
        }

        public void AprobarSolicitud(Reserva reserva)
        {
            if (reserva.Estado != EnumEstado.Pendiente)
            {
                throw new InvalidOperationException("Solo se pueden aprobar reservas en estado pendiente.");
            }

            reserva.Estado = EnumEstado.Aprobada;
            _sqlRepositorioReserva.ActualizarReserva(reserva);

            var mensaje = $"Su reserva del depósito {reserva.Deposito.Nombre} ha sido aprobada.";
            _servicioNotificacion.GenerarNotificacion(reserva, mensaje);
        }

        public void ProcesarPago(int reservaId)
        {
            var reserva = BuscarReservaPorId(reservaId);
            if (reserva == null)
            {
                throw new ArgumentNullException(nameof(reserva), "No se encontró la reserva especificada.");
            }

            if (reserva.EstadoPago == EnumEstadoPago.Capturado)
            {
                throw new InvalidOperationException("El pago ya ha sido procesado.");
            }

            reserva.EstadoPago = EnumEstadoPago.Reservado;
            _sqlRepositorioReserva.ActualizarReserva(reserva);
        }

        public byte[] GenerarReporteCSV()
        {
            var reservas = TraerListaReservas();
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    writer.WriteLine("DEPOSITO,RESERVA,PAGO");

                    foreach (var reserva in reservas)
                    {
                        var deposito = reserva.Deposito;
                        var linea = $"{deposito.Nombre},{reserva.FechaInicio.ToShortDateString()} - {reserva.FechaFin.ToShortDateString()},{reserva.EstadoPago}";
                        writer.WriteLine(linea);
                    }
                    writer.Flush();
                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] GenerarReporteTXT()
        {
            var reservas = TraerListaReservas();
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    writer.WriteLine("DEPOSITO\tRESERVA\tPAGO");

                    foreach (var reserva in reservas)
                    {
                        var deposito = reserva.Deposito;
                        var linea = $"{deposito.Nombre}\t{reserva.FechaInicio.ToShortDateString()} - {reserva.FechaFin.ToShortDateString()}\t{reserva.EstadoPago}";
                        writer.WriteLine(linea);
                    }
                    writer.Flush();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}


