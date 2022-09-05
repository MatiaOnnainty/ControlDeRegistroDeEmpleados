using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeAsistenciaPersonal
{
    public enum TipoRegistro
    {
        ingreso = 0,
        egreso = 1,
    }
    public class Registro_acceso
    {
        public DateTime Fecha { get; set; }
        public TipoRegistro Tipo { get; set; }
        public Registro_acceso(DateTime fecha, TipoRegistro tipo)
        {
            Fecha = fecha;
            Tipo = tipo;
        }
        public string Observaciones { get; set; }
    }
}