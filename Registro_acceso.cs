using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeRegistroDeEmpleados
{
    public enum TipoRegistro
    {
        ingreso = 0,
        egreso = 1,
    }

    public class Registro_acceso : Empleado
    {
        public Registro_acceso()
        {

        }
        //con esta clase registramos todos los accesos de la misma persona para poder 
        //calcular las horas trabajadas por día. Son los datos que deberíamos mostrar en un archivo excel
        public DateTime Fecha { get; set; }
        public TipoRegistro Tipo { get; set; }

        public Registro_acceso(DateTime fecha, TipoRegistro tipo)
        {
            Fecha = fecha;
            Tipo = tipo;
        }

    }
}

