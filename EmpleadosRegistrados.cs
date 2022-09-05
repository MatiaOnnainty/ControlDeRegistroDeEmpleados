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
    public partial class EmpleadosRegistrados : Form
    {
        public EmpleadosRegistrados()
        {
            InitializeComponent();
        }

        public Form1 FormularioPadre { get; set; }
        public DataTable DatosEmpleados { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            FormularioPadre.Show();
            this.Close();
        }

        private void EmpleadosRegistrados_Load_1(object sender, EventArgs e)
        {
            dataGridViewEmpleados.DataSource = DatosEmpleados;
            dataGridViewEmpleados.Columns[0].HeaderText = "DNI";
            dataGridViewEmpleados.Columns[2].Visible = false;
            dataGridViewEmpleados.Columns[3].Visible = false;
        }

        private void EmpleadosRegistrados_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            FormularioPadre.Show();
        }

        private void dataGridViewEmpleados_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            JornadaEmpleados jornadaEmpleados = new JornadaEmpleados();
            jornadaEmpleados.FormularioPadre = this;
            jornadaEmpleados.DNI = dataGridViewEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString();
            jornadaEmpleados.Nombre = dataGridViewEmpleados.Rows[e.RowIndex].Cells[1].Value.ToString();
            jornadaEmpleados.ShowDialog();
        }
    }
}
