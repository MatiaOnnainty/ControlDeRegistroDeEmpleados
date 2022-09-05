using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
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
        public List<Empleado> ListaEmpleados { get; set; }

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
            jornadaEmpleados.ListaEmpleados = ListaEmpleados;
            jornadaEmpleados.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string consulta;
            if (textBoxDni.Text == "")
            {
                consulta = "SELECT * FROM [Sheet1$]";
            }
            else
            {
                string valor = textBoxDni.Text;
                consulta = "SELECT * FROM [Sheet1$] WHERE [F1] LIKE '%" + valor + "%'";
            }

            //Definis la ruta
            String directorioArchivo = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx";
            //Definis el connectionString
            String conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO;\"";
            //string conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
            //Definis la Query 
            //String consulta = "Select * from [Hoja 1$]";
            //Instancias un nuevo objeto de tipo OleDbConnection y abris la conexión con .Open();
            OleDbConnection con = new OleDbConnection(conexion);
            con.Open();
            //Definisun Command pasándole tu query y el objeto de conexión
            OleDbCommand cmd = new OleDbCommand(consulta, con);
            //Por último creas un Adapter y se lo asignas a un DataSet
            OleDbDataAdapter db = new OleDbDataAdapter(cmd);
            DataTable ds = new DataTable();

            db.Fill(ds);
            con.Close();

            //tomamos la primera tabla que es el documento seleccionado y borramos todas las fillas nulas o vacías
            ds.AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
            ds.AcceptChanges();

            ds.AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
            ds.AcceptChanges();

            dataGridViewEmpleados.DataSource = ds.DefaultView;
            dataGridViewEmpleados.Columns[2].Visible = false;
            dataGridViewEmpleados.Columns[3].Visible = false;
        }
    }
}
