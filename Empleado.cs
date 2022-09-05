using ControlDeAsistenciaPersonal;
using DocumentFormat.OpenXml.Wordprocessing;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDeRegistroDeEmpleados
{
    public class Empleado
    {
        public Empleado()
        {

        }

        //nuevo
        private List<Registro_acceso> listaRegistros = new List<Registro_acceso>();
        private List<Jornada> Historial = new List<Jornada>();
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public double HorasSemanales { get; set; }
        public List<Registro_acceso> ListaRegistro { get => listaRegistros; set => listaRegistros = value; }


        private double CalcularHorasDia(DateTime fecha, List<Registro_acceso> lista)
        {
            double horasTrabajadas = 0;

            if (lista.Count == 1)
            {
                //throw new Exception("Falto marcar entrada o Salida");

                lista[0].Observaciones = lista[0].Tipo.ToString() == "egreso" ?
                    "marco solo el egreso. fecha -> " + lista[0].Fecha :
                    "marco solo el ingreso. fecha -> " + lista[0].Fecha;
                return 0;
            }

            Registro_acceso actual;
            Registro_acceso siguiente;

            for (int i = 0; i < lista.Count - 1; i++)
            {
                actual = lista[i];
                siguiente = lista[i + 1];

                TimeSpan diferenciaDeHoras = siguiente.Fecha.Subtract(actual.Fecha);

                if (actual.Tipo == TipoRegistro.ingreso && siguiente.Tipo == TipoRegistro.egreso && diferenciaDeHoras.TotalMinutes > 59)
                {
                    //caso ideal
                    horasTrabajadas += (Convert.ToDouble(diferenciaDeHoras.TotalHours));
                    i++;

                }
                else if (actual.Tipo == TipoRegistro.ingreso)
                {

                    lista[0].Observaciones += "No marcó salida. Día -> " + actual.Fecha + "\n";
                }
                else if (actual.Tipo == TipoRegistro.egreso)
                {
                    lista[0].Observaciones += "No marcó entrada. Día -> " + siguiente.Fecha + "\n";
                }
                else if (actual.Tipo == siguiente.Tipo && diferenciaDeHoras.Hours >= 1)
                {
                    try
                    {
                        TimeSpan diferenciaDeHoras2 = lista[i + 2].Fecha.Subtract(siguiente.Fecha);
                        //ignoramos la primera que aparece y seguimos con la segunda
                        if ((i + 2) < lista.Capacity && diferenciaDeHoras2.Minutes < 10)
                        {
                            //lista[0].Observaciones += "No marcó salida. Día -> " + actual.Fecha + "\n";
                            lista.Remove(lista[i + 1]);
                            i--;
                            continue;
                        }
                    }
                    catch (Exception)
                    {

                        lista[0].Observaciones += "Salida se marco como entrada. Día -> " + siguiente.Fecha + "\n";
                    }
                    
                }

            }
            this.HorasSemanales += Math.Round(horasTrabajadas,2);

            return Math.Round(horasTrabajadas, 2);
        }

        public void CalcularHorasTrabajadas()
        {
            //variables para llevar los calculos de un dia especifico
            DateTime fecha = DateTime.Today;
            List<Registro_acceso> registroDias = new List<Registro_acceso>();

            //Generamos archivos individuales por persona que registran todos sus movimientos de entradas y salidas
            //con observaciones
            SLDocument document = new SLDocument();
            DataTable dt = new DataTable();
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Horas Trabajadas", typeof(double));
            dt.Columns.Add("Observaciones", typeof(string));

            for (int i = 0; i < this.ListaRegistro.Count; i++)
            {
                Registro_acceso actual;

                actual = this.listaRegistros[i];

                //detecta cual es el primer registro
                if (fecha.Date == DateTime.Today)
                {
                    fecha = actual.Fecha;
                }
                //cambiamos de dia
                else if (fecha.Date != actual.Fecha.Date)
                {
                    Jornada jornada = new Jornada();
                    jornada.HorasTrabajadas = CalcularHorasDia(fecha, registroDias);
                    jornada.HorasSemanales += jornada.HorasTrabajadas;
                    jornada.Fecha = fecha;
                    jornada.Observaciones = registroDias[0].Observaciones;

                    this.Historial.Add(jornada);

                    //reseteamos las variables para el nueva fecha
                    fecha = actual.Fecha;
                    registroDias = new List<Registro_acceso>();
                }

                registroDias.Add(actual);

                if (i == this.ListaRegistro.Count - 1)
                {
                    Jornada jornada = new Jornada();
                    jornada.HorasTrabajadas = CalcularHorasDia(fecha, registroDias);
                    jornada.Fecha = fecha;
                    jornada.Observaciones = registroDias[0].Observaciones;

                    this.Historial.Add(jornada);
                }
            }

            foreach (var item in Historial)
            {
                dt.Rows.Add(item.Fecha.ToString(), item.HorasTrabajadas, item.Observaciones);
            }

            document.ImportDataTable(1, 1, dt, true);
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistrosEmpleados");
            document.SaveAs(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\RegistrosEmpleados\\" + this.Nombre + " - " + this.DNI + ".xlsx");
            //_ = File.Exists(Directory.GetCurrentDirectory() + "\\BD_EXCEL\\" + this.Nombre + " - " + this.Dni + ".xlsx") ? MessageBox.Show("empleado generado correctamente") : MessageBox.Show("no se generó el archivo");
        }


    }
}
