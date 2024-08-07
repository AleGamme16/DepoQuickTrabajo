using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DtoDisponibilidad
    {
        public int ID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DepositoID { get; set; }
    }
}
