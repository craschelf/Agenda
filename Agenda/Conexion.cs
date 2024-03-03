using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Agenda
{
    internal class Conexion
    {
        private MySqlConnection conexion;
        private string database = "Agenda";
        private string servidor = "localhost";
        private string usuario = "root"; // Usuario de MySQL
        private string password = "abc123."; // Contraseña de MySQL
        private string CadenaConexion;

        public Conexion()
        {
            CadenaConexion = "Server=" + servidor + "; database=" + database + "; uid=" + usuario + "; password=" + password + ";";
        }

        public MySqlConnection GetConexion()
        {
            if (conexion == null)
            {
                conexion = new MySqlConnection(CadenaConexion);
                conexion.Open();
            }
            return conexion;
        }
    }
}


