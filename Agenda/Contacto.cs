using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    internal class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Comentario { get; set; }
        public List<string> Telefonos { get; set; }
        public List<string> Emails { get; set; }

        public Contacto(int id, string nombre, string apellido1, string apellido2, string comentario, List<string> telefonos, List<string> emails)
        {
            Id = id;
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            Comentario = comentario;
            Telefonos = telefonos;
            Emails = emails;
        }

        //Este constructor se usará cuando agregemos un nuevo contacto ya que el id no hará falta porque será autoincremental y se asignará por defecto al añadir el contacto a la base de datos
        public Contacto(string nombre, string apellido1, string apellido2, string comentario, List<string> telefonos, List<string> emails)
        {
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            Comentario = comentario;
            Telefonos = telefonos;
            Emails = emails;
        }

        //Este constructor lo usaremos para borrar un contacto, ya que solo necesitaremos el id de ese contacto para borrar todos los datos relacionados
        public Contacto(int id) 
        {
            Id = id;
        }
    }
}
