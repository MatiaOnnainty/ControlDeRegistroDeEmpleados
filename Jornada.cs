using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeRegistroDeEmpleados
{
    public class Jornada
    {
        public Jornada()
        {
        }

        public DateTime Fecha { get; set; }
        public double HorasTrabajadas { get; set; }
        public string Observaciones { get; set; }
    }
}
