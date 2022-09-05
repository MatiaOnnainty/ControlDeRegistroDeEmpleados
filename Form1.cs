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
            string path = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\REGISTRO FINAL.xlsx";

            SLDocument document = new SLDocument();
            DataTable dt = new DataTable();


            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[0].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[1].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[2].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[3].Value.ToString(), typeof(string));

            List<Empleado> listaFinal = new List<Empleado>();

            string dni, nombre, fecha, tipo;
            Empleado persona = null;

            //lectura del datagridview y procesamiento de datos para calcular horas trabajadas por empleado
            for (int i = 1; i < dataGridViewPlanilla.Rows.Count; i++)
            {
                dni = dataGridViewPlanilla.Rows[i].Cells[0].Value.ToString();
                nombre = dataGridViewPlanilla.Rows[i].Cells[1].Value.ToString();
                fecha = dataGridViewPlanilla.Rows[i].Cells[2].Value.ToString();
                tipo = dataGridViewPlanilla.Rows[i].Cells[3].Value.ToString();


                if (persona == null)
                {
                    persona = new Empleado();
                    persona.Nombre = nombre;
                    persona.DNI = dni;
                }
                else if (persona.DNI != dni)
                {
                    dt.Rows.Add(dni, nombre, fecha, tipo);

                    //cambio de persona entonces procesamos la anterior
                    persona.CalcularHorasTrabajadas();

                    listaFinal.Add(persona);

                    persona = new Empleado();
                    persona.Nombre = nombre;
                    persona.DNI = dni;
                }

                TipoRegistro tipoRegistro = tipo == "Registro de entrada" ? TipoRegistro.ingreso : TipoRegistro.egreso;
                persona.ListaRegistros.Add(new Registro_acceso(Convert.ToDateTime(fecha), tipoRegistro));


                //CONDICION PARA EL VALOR INICIAL
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dni, nombre, fecha, tipo);
                }


                //CONDICION PARA EL VALOR FINAL (PARA QUE SE GUARDEN)
                if (i == dataGridViewPlanilla.Rows.Count - 1)
                {
                    //dt.Rows.Add(dni, nombre, fecha, tipo);
                    //este if es para que me tome si hay un solo dni en el registro
                    persona.CalcularHorasTrabajadas();
                    listaFinal.Add(persona);
                }

            }

            DtEmpleados = dt;
            document.ImportDataTable(1, 1, dt, true);

            document.SaveAs(path);
            //_ = File.Exists(path) ? MessageBox.Show("empleado generado correctamente") : MessageBox.Show("no se generó el archivo");

            MessageBox.Show("¡Registros Generados con éxito!");
            BotonCrearArchivoExcel.Visible = true;
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
    }
    
}
