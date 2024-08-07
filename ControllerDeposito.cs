using Backend.DTOs;
using Backend.Services;
using System.Collections.Generic;

namespace Backend.Controllers
{
    public class ControllerDeposito
    {
        private readonly ServicioDeposito _servicioDeposito;

        public ControllerDeposito(ServicioDeposito servicioDeposito)
        {
            _servicioDeposito = servicioDeposito;

        }

        public void AgregarDeposito(DtoDeposito dtoDeposito)
        {
            _servicioDeposito.AgregarDeposito(dtoDeposito);
        }

        public List<DtoDeposito> ObtenerDepositos()
        {
            return _servicioDeposito.ObtenerDepositos();
        }

        public void BorrarDeposito(int id)
        {
            _servicioDeposito.BorrarDeposito(id);
        }

        public bool EstaDisponible(int depositoID, DateTime fechaInicio, DateTime fechaFin)
        {
            return _servicioDeposito.EstaDisponible(depositoID, fechaInicio, fechaFin);
        }

        public List<DtoDeposito> ObtenerDepositosDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            var depositosDisponibles = _servicioDeposito.ObtenerDepositosDisponibles(fechaInicio, fechaFin);
            return depositosDisponibles.Select(MapearEntidadADtoDeposito).ToList();
        }

        private DtoDeposito MapearEntidadADtoDeposito(Deposito deposito)
        {
            return new DtoDeposito
            {
                ID = deposito.ID,
                Area = deposito.Area,
                Tamano = deposito.Tamano,
                Climatizado = deposito.Climatizado,
                Nombre = deposito.Nombre,
                Disponibilidades = deposito.Disponibilidades.Select(d => new DtoDisponibilidad
                {
                    FechaInicio = d.FechaInicio,
                    FechaFin = d.FechaFin,
                    DepositoID = d.DepositoID
                }).ToList()
            };
        }

        public bool VerificarDisponibilidad(int depositoId, DateTime fechaInicio, DateTime fechaFin)
        {
            return _servicioDeposito.VerificarDisponibilidad(depositoId, fechaInicio, fechaFin);
        }
        public void AgregarDisponibilidad(DtoDisponibilidad dtoDisponibilidad)
        {
            var disponibilidad = new Disponibilidad
            {
                DepositoID = dtoDisponibilidad.DepositoID,
                FechaInicio = dtoDisponibilidad.FechaInicio,
                FechaFin = dtoDisponibilidad.FechaFin
            };
            _servicioDeposito.AgregarDisponibilidad(disponibilidad);
        }
    }
}

