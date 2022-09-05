using ControlDeAsistenciaPersonal;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
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
                openFileDialog.InitialDirectory = "C:\\Users\\Bangho\\Descargas";
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
            //Definis el connectionString
            String conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorioArchivo + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
            //Definis la Query 
            String consulta = "Select * from [Hoja 1$]";
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

        private void BotonGenerarRegistrosJornada_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx";

            SLDocument document = new SLDocument();
            DataTable dt = new DataTable();


            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[0].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[1].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[2].Value.ToString(), typeof(string));
            dt.Columns.Add(dataGridViewPlanilla.Rows[0].Cells[3].Value.ToString(), typeof(string));

            List<Empleado> listaFinal = new List<Empleado>();
            int contador = 0;


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
                persona.ListaRegistro.Add(new Registro_acceso(Convert.ToDateTime(fecha), tipoRegistro));


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

            BotonVerListaEmpleados.Enabled = true;
        }

        private void BotonVerListaEmpleados_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmpleadosRegistrados empleadosRegistrados = new EmpleadosRegistrados();
            empleadosRegistrados.FormularioPadre = this;

            if (DtEmpleados == null && File.Exists(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx"))
            {
                //Definis la ruta
                String directorioArchivo = Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistroFinal.xlsx";
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

        private void textBoxFiltradoPorDNI_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
    
}
