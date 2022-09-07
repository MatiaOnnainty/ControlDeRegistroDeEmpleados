using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlDeRegistroDeEmpleados
{
    public partial class JornadaEmpleados : Form
    {
        public JornadaEmpleados()
        {
            InitializeComponent();
        }

        public EmpleadosRegistrados FormularioPadre { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public List<Empleado> ListaEmpleados { get; set; }

        Empleado em = new Empleado();

        private void JornadaEmpleados_Load(object sender, EventArgs e)
        {
            foreach (Empleado empleado in ListaEmpleados)
            {
                if (DNI == empleado.DNI)
                {
                    //em.HorasSemanales = empleado.HorasSemanales;
                    em = empleado;
                    break;
                }
            }
            labelDNI.Text = em.DNI;
            labelNombre.Text = em.Nombre;
            labelHorasSemanales.Text = "Horas Semanales: " + em.HorasSemanales.ToString();
            labelFaltas.Text = "Asistencia Semanal: " + em.Inasistencia.ToString();

            dataGridViewRegistros.Columns.Add("fecha","FECHA");
            dataGridViewRegistros.Columns.Add("horas", "HORAS");
            dataGridViewRegistros.Columns.Add("observaciones", "OBSERVACIONES");

            string observaciones;
            
            foreach (Jornada item in em.Historial)
            {
                TimeSpan tiempo = new TimeSpan(Convert.ToInt32(Math.Floor(item.HorasTrabajadas)),
                Convert.ToInt32((item.HorasTrabajadas - Math.Floor(item.HorasTrabajadas)) * 60),
                0);
                observaciones = (item.Observaciones != null) ? item.Observaciones.ToString() : ""; 
                dataGridViewRegistros.Rows.Add(item.Fecha.ToString("dd/MM/yyyy"),tiempo.ToString(), observaciones);  
            }
            
        }

        private void JornadaEmpleados_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            FormularioPadre.Show();
        }

        private void BotonVerCalendario_Click(object sender, EventArgs e)
        {
            Calendario calendario = new Calendario();
            calendario.empleadoActual = em;
            calendario.ShowDialog();
        }

        private void BotonSalir_Click(object sender, EventArgs e)
        {
            FormularioPadre.Show();
            this.Close();
        }
    }
}
