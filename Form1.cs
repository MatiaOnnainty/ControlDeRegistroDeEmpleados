using ControlDeAsistenciaPersonal;
using ControlDeRegistroDeEmpleados.modulo_calculo;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace ControlDeRegistroDeEmpleados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //string rutaBDEXCEL = Directory.GetCurrentDirectory() + "\\BD_EXCEL";
        private DataTable DtEmpleados;

        private void BotonCargarArchivo_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var fileName = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    fileName = openFileDialog.SafeFileName;

                    //Metodo que crea una copia del archivo y carga sus datos en un datagridview
                    CrearCopiaDeArchivo(filePath, fileName);

                    //Si se selecciona un archivo correcto entonces habilitamos las opciones de filtrado y carga de datos
                    
                    dataGridViewPlanilla.Visible = true;
                    BotonVerListaEmpleados.Visible = true;
                    BotonGenerarRegistrosJornada.Visible = true;
                    BotonGenerarRegistrosJornada.Enabled = true;
                    BotonCargarArchivo.Location = new System.Drawing.Point(13, 13);
                }
            }
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



            //llamamos al metodo que carga el datagridview
            CargarDatos(destFile.ToString());
        }

        public void CargarDatos(string path)
        {
            //Definis la ruta
            String directorioArchivo = path;
            SLDocument sl = new SLDocument(path);
            List<string> nombreHoja = sl.GetSheetNames();
            //Definis el connectionString
            String conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
            //Definis la Query 
            String consulta = "Select * from ["+ nombreHoja[0] +"$]";
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
            ds.Columns[0].ColumnName = "dni";
            ds.AcceptChanges();

            dataGridViewPlanilla.DataSource = ds.DefaultView;
            //sort nos permite tomar una columna y ordenarla de forma ascendente o descendente
            //dataGridViewRegistros.Sort(dataGridViewRegistros.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            /*SLDocument sl = new SLDocument(path);

            int iRow = 4;

            DataTable dt = new DataTable();
            if (dataGridViewPlanilla.Rows.Count == 0)
            {
                dataGridViewPlanilla.Columns.Add("dni","DNI");
                dataGridViewPlanilla.Columns.Add("nombre", "Nombre y Apellido");
                dataGridViewPlanilla.Columns.Add("horas", "Horas");
                dataGridViewPlanilla.Columns.Add("observaciones", "Observaciones");
            }

            dt.Columns.Add("DNI");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Horas");
            dt.Columns.Add("Observaciones");

            while (!String.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                dt.Rows.Add();

                dt.Rows[iRow - 4]["DNI"] = sl.GetCellValueAsString(iRow, 1);
                dt.Rows[iRow - 4]["Nombre"] = sl.GetCellValueAsString(iRow, 2).ToUpperInvariant();
                dt.Rows[iRow - 4]["Horas"] = sl.GetCellValueAsString(iRow, 3);
                dt.Rows[iRow - 4]["Observaciones"] = sl.GetCellValueAsString(iRow, 4);

                dataGridViewPlanilla.Rows.Add(dt.Rows[iRow - 4][0], dt.Rows[iRow - 4][1], dt.Rows[iRow - 4][2], dt.Rows[iRow - 4][3]);
                iRow++;
            }


            //dataGridViewPlanilla.DataSource = dt;*/

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx"))
            {
                dataGridViewPlanilla.Visible = true;
                BotonVerListaEmpleados.Visible = true;
                BotonGenerarRegistrosJornada.Visible = true;
                BotonCargarArchivo.Location = new System.Drawing.Point(13, 13);
            }
            else
            {
                dataGridViewPlanilla.Visible = false;
                BotonVerListaEmpleados.Visible = false;
                BotonGenerarRegistrosJornada.Visible = false;
                BotonCargarArchivo.Location = new System.Drawing.Point(158, 146);
            }
            
        }

        private List<Empleado> listaFinal = new List<Empleado>();

        private void BotonGenerarRegistrosJornada_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx";
            SLDocument document = new SLDocument();
            Empleado persona = null;
            string dni, nombre, fecha, tipo;

            //lectura del datagridview y procesamiento de datos para calcular horas trabajadas por empleado
            for (int i = 0; i < dataGridViewPlanilla.Rows.Count; i++)
            {
                dni = dataGridViewPlanilla.Rows[i].Cells[0].Value.ToString();
                nombre = dataGridViewPlanilla.Rows[i].Cells[1].Value.ToString();
                fecha = dataGridViewPlanilla.Rows[i].Cells[2].Value.ToString();
                tipo = dataGridViewPlanilla.Rows[i].Cells[3].Value.ToString();

                if (persona == null)
                {
                    persona = ComprobarExistenciaEmpleado(nombre, dni);
                }
                else if (persona.DNI != dni)
                {
                    //cambio de persona entonces procesamos la anterior
                    persona.CalcularHorasTrabajadas();
                    persona.Inasistencia = CalcularAsistencia(persona);

                    listaFinal.Add(persona);

                    persona = ComprobarExistenciaEmpleado(nombre, dni);
                }

                TipoRegistro tipoRegistro = tipo == "Registro de entrada" ? TipoRegistro.ingreso : TipoRegistro.egreso;
                persona.ListaRegistro.Add(new Registro_acceso(Convert.ToDateTime(fecha), tipoRegistro));

                //CONDICION PARA EL VALOR FINAL (PARA QUE SE GUARDEN)
                if (i == dataGridViewPlanilla.Rows.Count - 1)
                {
                    //dt.Rows.Add(dni, nombre, fecha, tipo);
                    //este if es para que me tome si hay un solo dni en el registro+

                    persona.CalcularHorasTrabajadas();
                    persona.Inasistencia = CalcularAsistencia(persona);
                    listaFinal.Add(persona);
                }
            }

            MessageBox.Show("¡Registros Generados con éxito!");

            BotonVerListaEmpleados.Enabled = true;
            dataGridViewPlanilla.DataSource = null;
            BotonGenerarRegistrosJornada.Enabled = false;
        }

        private int CalcularAsistencia(Empleado persona)
        {
            int inasistencia = 0, registros = 0;
            int total = 0;

            foreach (Jornada item in persona.Historial)
            {
                if (item.HorasTrabajadas == 0)
                {
                    inasistencia++;
                }
                registros++;
            }

            total = 5 - inasistencia -(5-registros);

            return total;
        }

        private Empleado ComprobarExistenciaEmpleado(string nombre, string dni)
        {
            Empleado em = new Empleado();
            em = null;

            foreach (Empleado item in listaFinal)
            {
                if (item.DNI == dni)
                {
                    em = item;
                    em.ListaRegistro.Clear();
                    listaFinal.Remove(item);
                    em.HorasSemanales = 0;
                    em.Inasistencia = 0;
                    return em;
                }

            }
            
            em = new Empleado();
            em.Nombre = nombre;
            em.DNI = dni;
            return em;
        }

        private void BotonVerListaEmpleados_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmpleadosRegistrados empleadosRegistrados = new EmpleadosRegistrados();
            empleadosRegistrados.FormularioPadre = this;

            //LISTA DE OBJETOS PERSONA (LIST<T>)
            empleadosRegistrados.ListaEmpleados = listaFinal;
            empleadosRegistrados.ShowDialog();
        }

        private void textBoxFiltradoPorDNI_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
    
}
