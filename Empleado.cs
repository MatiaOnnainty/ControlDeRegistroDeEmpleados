using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeRegistroDeEmpleados
{
    public class Empleado
    {
        public Empleado()
        {

        }

        //con esta clase registramos los datos de cada empleado para poder manipular los datos
        //registrados de ingreso/egreso
        public string Nombre { get; set; }
        public string DNI { get; set; }

        private List<Registro_acceso> listaRegistros = new List<Registro_acceso>();
        private List<Jornada> Historial = new List<Jornada>();


        public List<Registro_acceso> ListaRegistros { get => listaRegistros; set => listaRegistros = value; }
        public List<Jornada> Historial1 { get => Historial; set => Historial = value; }

        public void CalcularHorasTrabajadas()
        {
            //variables para llevar los calculos de un dia especifico
            DateTime fecha = DateTime.Today;

            string observaciones = "";

            List<Registro_acceso> registroDias = new List<Registro_acceso>();


            for (int i = 0; i < this.ListaRegistros.Count; i++)
            {

                Registro_acceso actual = this.listaRegistros[i];
                Registro_acceso siguiente = this.listaRegistros[i + 1];

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
                    jornada.Fecha = fecha;
                    jornada.Observaciones = observaciones;


                    this.Historial.Add(jornada);

                    //reseteamos las variables para el nueva fecha
                    fecha = actual.Fecha;
                    observaciones = "";
                    registroDias = new List<Registro_acceso>();
                }

                registroDias.Add(actual);

            }
        }


        private double CalcularHorasDia(DateTime fecha, List<Registro_acceso> lista)
        {
            double horasTrabajadas = 0;

            if (lista.Count == 1)
            {
                throw new Exception("Falto marcar entrada o Salida");
            }

            DateTime actual;
            DateTime siguiente;

            for (int i = 0; i < lista.Count - 1; i++)
            {
                actual = lista[i].Fecha;
                siguiente = lista[i + 1].Fecha;

                TimeSpan diferenciaDeHoras = siguiente.Subtract(actual);

                if (lista[i].Tipo != lista[i + 1].Tipo && diferenciaDeHoras.Hours > 1)
                {
                    //caso ideal
                    horasTrabajadas += (Convert.ToDouble(diferenciaDeHoras));
                }
                else if (lista[i].Tipo == lista[i + 1].Tipo && diferenciaDeHoras.Hours < 1)
                {
                    //ignoramos la primera que aparece y seguimos con la segunda
                    //actual
                }
                else if (lista[i].Tipo != lista[i + 1].Tipo && diferenciaDeHoras.Hours < 1)
                {
                    //tomamos la segunda 
                }





                //Registro_acceso actual = this.listaRegistros[i];
                //Registro_acceso siguiente = this.listaRegistros[i + 1];
                /*
                if (actual.Fecha.Date == siguiente.Fecha.Date)
                {
                    if (actual.Tipo == TipoRegistro.ingreso && siguiente.Tipo == TipoRegistro.egreso)
                    {
                        TimeSpan diferenciaDeHoras = siguiente.Fecha.Subtract(actual.Fecha);
                        horasTrabajadas += (diferenciaDeHoras.TotalMinutes) / 60;

                    }
                }
                else
                {




                }*/
            }

            return horasTrabajadas;

        }


    }
}
