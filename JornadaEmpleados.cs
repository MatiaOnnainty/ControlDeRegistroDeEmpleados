using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            em.DNI = DNI;
            labelDNI.Text = DNI;
            labelNombre.Text = Nombre;
            

            foreach (Empleado empleado in ListaEmpleados)
            {
                if (em.DNI == empleado.DNI)
                {
                    //em.HorasSemanales = empleado.HorasSemanales;
                    em = empleado;
                }
            }
            labelHorasSemanales.Text = "Horas Semanales: " + em.HorasSemanales.ToString();

            //Definis la ruta
            String directorioArchivo = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistrosEmpleados\\" + this.Nombre + " - " + this.DNI + ".xlsx";
            //Definis el connectionString
            String conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
            //Definis la Query 
            String consulta = "Select * from [Sheet1$]";
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

            dataGridViewRegistros.DataSource = ds.DefaultView;
            //sort nos permite tomar una columna y ordenarla de forma ascendente o descendente
            //dataGridViewRegistros.Sort(dataGridViewRegistros.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
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
    }
}
