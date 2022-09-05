using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using objetoExcel = Microsoft.Office.Interop.Excel;
using ControlDeAsistenciaPersonal;
using ControlDeRegistroDeEmpleados.modulo_calculo;
using SpreadsheetLight;
using DataTable = System.Data.DataTable;

namespace ControlDeRegistroDeEmpleados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string rutaBDEXCEL = Directory.GetCurrentDirectory() + "\\BD_EXCEL";

        DataView ImportarDatos(string nombreArchivo)
        {
            string conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0;'", nombreArchivo);

            OleDbConnection conector = new OleDbConnection(conexion);

            conector.Open();

            OleDbCommand consulta = new OleDbCommand("select * from [Hoja1$]", conector);

            OleDbDataAdapter adaptador = new OleDbDataAdapter
            {
                SelectCommand = consulta
            };

            DataSet ds = new DataSet();

            adaptador.Fill(ds);

            conector.Close();

            ds.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
            ds.Tables[0].AcceptChanges();

            return ds.Tables[0].DefaultView;
        }

        private void BotonCargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel | *.xls;*.xlsx;",
                Title = "Seleccionar Archivo"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridViewPlanilla.Visible = true;
                dataGridViewEscondido.Visible = false;
                BotonCrearArchivoExcel.Visible = true;
                BotonGenerarRegistrosJornada.Visible = true;
                panelFiltrado.Visible = true;
                BotonCargarArchivo.Location = new System.Drawing.Point(12, 12);

                dataGridViewEscondido.DataSource = ImportarDatos(openFileDialog.FileName);

                int contador = dataGridViewEscondido.Rows.Count;
                string dni, nombre, fecha, tipo;

                if (dataGridViewPlanilla.Rows.Count == 0)
                {
                    dataGridViewPlanilla.Columns.Add("dni", "Dni");
                    dataGridViewPlanilla.Columns.Add("nombre", "Nombre y Apellido");
                    dataGridViewPlanilla.Columns.Add("fecha", "Dia y hora");
                    dataGridViewPlanilla.Columns.Add("tipo", "Observacion");
                }
                for (int i = 0; i < contador - 1; i++)
                {
                    dni = dataGridViewEscondido.Rows[i].Cells[0].Value.ToString();
                    nombre = dataGridViewEscondido.Rows[i].Cells[1].Value.ToString();
                    fecha = dataGridViewEscondido.Rows[i].Cells[2].Value.ToString();
                    tipo = dataGridViewEscondido.Rows[i].Cells[3].Value.ToString();

                    dataGridViewPlanilla.Rows.Add(dni, nombre, fecha, tipo);
                }
                dataGridViewEscondido.DataSource = "";
                CrearCopiaDeArchivo(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewPlanilla.Visible = false;
            dataGridViewEscondido.Visible = false;
            BotonCrearArchivoExcel.Visible = false;
            BotonGenerarRegistrosJornada.Visible = false;
            panelFiltrado.Visible = false;
            BotonCargarArchivo.Location = new System.Drawing.Point(158, 146);
        }

        private DataTable DtEmpleados;
        //esto agregué por si hay problemas con el github
        private void BotonGenerarRegistrosJornada_Click(object sender, EventArgs e)
        {

        }

        private void BotonCrearArchivoExcel_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmpleadosRegistrados empleadosRegistrados = new EmpleadosRegistrados();
            empleadosRegistrados.FormularioPadre = this;


            if (DtEmpleados == null && File.Exists(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\REGISTRO FINAL.xlsx"))
            {
                //Definis la ruta
                String directorioArchivo = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\REGISTRO FINAL.xlsx";
                //Definis el connectionString
                String conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
                //Definis la Query 
                String consulta = "Select * from [Hoja1$]";
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

                DtEmpleados = ds;

            }
            empleadosRegistrados.DatosEmpleados = DtEmpleados;
            empleadosRegistrados.ShowDialog();
        }
        public void CrearCopiaDeArchivo(string path, string nombre)
        {
            SLDocument sl = new SLDocument();

            string fileName = nombre;
            string sourcePath = path;
            string targetPath = Directory.GetCurrentDirectory() + "\\BD_EXCEL";

            // sourceFile es la ubicación del archivo que seleccionamos, y destFile es la ubicación donde guardamos
            // la copia y el nombre que queremos que lleve.
            string sourceFile = Path.Combine(sourcePath, "");
            string destFile = Path.Combine(targetPath, "RegistroFinal.xlsx");

            // crea el directorio si no existe.
            Directory.CreateDirectory(targetPath);

            // hace la copia del archivo pasando la ubicación de origen y destino. con true decimos que sobreescriba el documento
            File.Copy(sourceFile, destFile, true);

            //guardamos el documento que va a estar en nuestro proyecto
            sl = new SLDocument(destFile);
        }
        private void BotonGenerarRegistrosJornada_Click_1(object sender, EventArgs e)
        {

        }
    }
    
}
