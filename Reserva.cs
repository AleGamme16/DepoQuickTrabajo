using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Backend.enums;

namespace Backend
{
    public class Reserva
    {
        public Cliente Cliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }
        public EnumEstado Estado { get; set; }
        public string? MotivoRechazo { get; set; }
        public Deposito Deposito { get; set; }
        public int ID { get; private set; }
        public EnumEstadoPago? EstadoPago { get; set; }

        public Reserva(Cliente cliente, DateTime fechaInicio, DateTime fechaFin, decimal costo,
            EnumEstado estado, string motivoDeRechazo, Deposito deposito)
        {

            Cliente = cliente;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Costo = costo;
            Estado = estado;
            MotivoRechazo = motivoDeRechazo;
            Deposito = deposito;

            EstadoPago = EnumEstadoPago.Abonar;
        }

        public Reserva() { }

        //public Reserva() 
        //{ 
        //    Cliente = new Cliente();
        //    FechaInicio = DateTime.Now.AddDays(1);  
        //    FechaFin = DateTime.Now.AddDays(14);
        //    Costo = 3300;
        //    Estado = EnumEstado.Pendiente;
        //    MotivoRechazo = "";
        //    Deposito = new Deposito();
            
        //    ContadorDeID++;
        //    ID = ContadorDeID;
        //}
        private static void ValidarFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio < DateTime.Today)
            {
                throw new ArgumentException("La fecha de inicio no puede ser" +
                    " anterior a la fecha de hoy.");
            }
            else if (fechaFin < fechaInicio)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a" +
                    "la fecha de inicio.");
            }
        }

        public override string ToString()
        {
            return $"Cliente: {Cliente}, Inicio de reserva: {FechaInicio}, " +
                $"Fin de la Reserva: {FechaFin}, $: {Costo}, Estado: {Estado}," +
                $"Motivo de rechazo: {MotivoRechazo}, Deposito: {Deposito}";
        }

    }

}



