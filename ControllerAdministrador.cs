using Backend.Context;
using Backend.DTOs;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Controllers;

public class ControllerAdministrador
{
    public ServicioReservasAdmin _serviciosReservaAdmin;
    public ServicioReserva _serviciosReserva;   
    private Reserva MapearDtoReservaAEntidad(DtoReserva unDtoReserva)
    {

        Reserva nuevaReserva = new Reserva();

        nuevaReserva.Cliente = (Cliente)_serviciosReserva.BuscarUsuario(unDtoReserva.ClienteID); 
        nuevaReserva.Deposito = _serviciosReserva.RetornarDepositoPorId(unDtoReserva.DepositoID); 

        nuevaReserva.FechaInicio = unDtoReserva.FechaInicio;
        nuevaReserva.FechaFin = unDtoReserva.FechaFin;
        nuevaReserva.Costo = unDtoReserva.Costo;
        nuevaReserva.Estado = unDtoReserva.Estado;
        nuevaReserva.EstadoPago = unDtoReserva.EstadoPago;

        return nuevaReserva;

    }

    public void AprobarSolicitud(DtoReserva unDtoReserva)
    {
        Reserva reservaSeleccionada = MapearDtoReservaAEntidad(unDtoReserva);
        _serviciosReservaAdmin.AprobarSolicitudReserva(reservaSeleccionada);
    }


    public void RechazarSolicitud(DtoReserva unDtoReserva)
    {
        Reserva reservaSeleccionada = MapearDtoReservaAEntidad(unDtoReserva);
        _serviciosReservaAdmin.RechazarSolicitud(reservaSeleccionada);
    }

    public bool EsUnaSolicitudPendiente(DtoReserva unDtoReserva)
    {
        Reserva reservaSeleccionada = MapearDtoReservaAEntidad(unDtoReserva);
        return _serviciosReservaAdmin.EsUnaSolicitudPendiente(reservaSeleccionada);
    }

    public bool ReservaEstaActiva(DtoReserva unDtoReserva)
    {
        Reserva reservaSeleccionada = MapearDtoReservaAEntidad(unDtoReserva);
        return _serviciosReservaAdmin.ReservaEstaActiva(reservaSeleccionada);
    }

}
