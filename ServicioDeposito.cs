using Backend.SQL;
using Backend.DTOs;
using System.Collections.Generic;
using Backend.ManejoDeRepositorios;
using System.Text.RegularExpressions;

namespace Backend.Services
{
    public class ServicioDeposito
    {
        private readonly SqlRepositorioDeposito _sqlRepositorioDeposito;
        private readonly SqlRepositorioPromocion _sqlRepositorioPromocion;
        private readonly ServicioSessionLogic _sessionLogic;

        public ServicioDeposito(SqlRepositorioDeposito sqlRepositorioDeposito, SqlRepositorioPromocion sqlRepositorioPromocion, ServicioSessionLogic sessionLogic)
        {
            _sqlRepositorioDeposito = sqlRepositorioDeposito;
            _sqlRepositorioPromocion = sqlRepositorioPromocion;
            _sessionLogic = sessionLogic;
        }

        public bool ValidarNombreDeposito(string nombre)
        {
            if (nombre == null || nombre.Length > 60)
            {
                return false;
            }

            string pattern = @"^[a-zA-Z]+$";

            return Regex.IsMatch(nombre, pattern);
        }

        public void AgregarDeposito(DtoDeposito dtoDeposito)
        {
            Promocion promo = null;
            if (dtoDeposito.PromocionId.HasValue)
            {
                promo = _sqlRepositorioPromocion.RetornarPromocionPorId(dtoDeposito.PromocionId.Value);
            }

            if(!ValidarNombreDeposito(dtoDeposito.Nombre)) {
                throw new InvalidOperationException("El nombre del deposito no es valido.");
            }

            var deposito = new Deposito
            {
                Area = dtoDeposito.Area,
                Tamano = dtoDeposito.Tamano,
                Climatizado = dtoDeposito.Climatizado,
                Promo = promo,
                Nombre = dtoDeposito.Nombre,
                ID= dtoDeposito.ID,
            };
            _sqlRepositorioDeposito.AgregarDeposito(deposito);

            var usuarioActual = _sessionLogic.UsuarioActual;
            if (usuarioActual == null)
            {
                throw new ArgumentNullException(nameof(usuarioActual), "El usuario actual no puede ser nulo.");
            }
        }

        public List<DtoDeposito> ObtenerDepositos()
        {
            var depositos = _sqlRepositorioDeposito.ObtenerDepositos();
            var dtoDepositos = new List<DtoDeposito>();

            foreach (var deposito in depositos)
            {
                dtoDepositos.Add(new DtoDeposito
                {
                    ID = deposito.ID,
                    Area = deposito.Area,
                    Tamano = deposito.Tamano,
                    Climatizado = deposito.Climatizado,
                    PromocionId = deposito.Promo?.ID
                });
            }

            return dtoDepositos;
        }

        public void BorrarDeposito(int id)
        {
            var deposito = _sqlRepositorioDeposito.RetornarDepositoPorId(id);
            if (deposito != null)
            {
                _sqlRepositorioDeposito.BorrarDeposito(deposito);

                var usuarioActual = _sessionLogic.UsuarioActual;
                if (usuarioActual == null)
                {
                    throw new ArgumentNullException(nameof(usuarioActual), "El usuario actual no puede ser nulo.");
                }
            }
        }

        public List<Deposito> ObtenerDepositosDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            return _sqlRepositorioDeposito.ObtenerDepositosDisponibles(fechaInicio, fechaFin);
        }

        public bool EstaDisponible(int depositoID, DateTime fechaInicio, DateTime fechaFin)
        {
            return _sqlRepositorioDeposito.EstaDisponible(depositoID, fechaInicio, fechaFin);
        }

        public void AgregarDisponibilidad(Disponibilidad disponibilidad)
        {
            _sqlRepositorioDeposito.AgregarDisponibilidad(disponibilidad);
        }

        public bool HayReservaEnFecha(int depositoID, DateTime fechaInicio, DateTime fechaFin)
        {
            return _sqlRepositorioDeposito.HayReservaEnFecha(depositoID, fechaInicio, fechaFin);
        }

        public bool VerificarDisponibilidad(int depositoId, DateTime fechaInicio, DateTime fechaFin)
        {
            //var disponibilidades = _sqlRepositorioDeposito.ObtenerDisponibilidadesPorDepositoId(depositoId);

            return !_sqlRepositorioDeposito.HayReservaEnFecha(depositoId, fechaInicio, fechaFin);
        }

    }
}

