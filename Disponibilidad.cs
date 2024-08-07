using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Disponibilidad
    {
        public int ID { get; set; }
        public int DepositoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Disponibilidad()
        {

        }
    }
}
