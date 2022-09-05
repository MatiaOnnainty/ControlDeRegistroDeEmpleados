using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDeRegistroDeEmpleados
{
    public partial class Calendario : Form
    {
        public Calendario()
        {
            InitializeComponent();
        }
        public Empleado empleadoActual { get; set; }
        private void Calendario_Load(object sender, EventArgs e)
        {
            string[] fechas = new string[empleadoActual.Historial.Count];

            for (int i = 0; i < empleadoActual.Historial.Count - 1; i++)
            {
                calendar.AddBoldedDate(empleadoActual.Historial[i].Fecha);
            }
        }
    }
}
