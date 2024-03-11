using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    public class Evento
    {
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }

        public Evento(DateTime fecha, string contenido) { 
            Fecha = fecha;
            Contenido = contenido;
        }
    }
}
