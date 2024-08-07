using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    
    public enum EnumEstado
    {
        Rechazada = 0,
        Pendiente = 1,
        Aprobada = 2,
    }

    public enum EnumArea
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
    }

    public enum EnumTamano
    {
        Pequeno = 0,
        Mediano = 1,
        Grande = 2,
    }

    public enum EnumRol
    {
        Cliente,
        Administrador,
    }

    public enum EnumEstadoPago
    {
        Abonar,
        Reservado,
        Capturado,
    }
}
