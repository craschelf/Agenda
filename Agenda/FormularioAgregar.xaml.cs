using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Agenda
{
    /// <summary>
    /// Lógica de interacción para Formulario.xaml
    /// </summary>
    public partial class Formulario : Window
    {
        private Conexion mConexion;
        public Formulario()
        {
            InitializeComponent();
            mConexion = new Conexion();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos de los TextBox
            string nombre = NombreTextBox.Text;
            string apellido1 = Apelido1TextBox.Text;
            string apellido2 = Apelido2TextBox.Text;
            string comentario = ComentarioTextBox.Text;
            string telefonosText = TelefonoTextBox.Text;
            string emailText = EmailTextBox.Text;
            
            
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(telefonosText))
            {
                MessageBox.Show("Se debe agregar al menos un nombre y un teléfono de contacto");
                return; // Salir del método si el nombre está vacío
            }

            // Dividir los números de teléfono y las direcciones de correo electrónico en listas
            List<string> telefonos = new List<string>(telefonosText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> emails = new List<string>(emailText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // Llamar al método para guardar los datos en la base de datos
            AgregarDatosEnBaseDeDatos(nombre, apellido1, apellido2, comentario, telefonos, emails);

            

        }

        private void AgregarDatosEnBaseDeDatos(string nombre, string apellido1, string apellido2, string comentario, List<string> telefonos, List<string> emails)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                try
                {
                    

                    // Insertar los datos principales en la tabla de contactos
                    string query = "INSERT INTO contacto (nome, apelido1, apelido2, comentario) VALUES (@Nombre, @Apellido1, @Apellido2, @Comentario)";
                    MySqlCommand command = new MySqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido1", (object)apellido1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Apellido2", (object)apellido2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Comentario", (object)comentario ?? DBNull.Value);
                    command.ExecuteNonQuery();

                    // Obtener el ID del contacto recién insertado
                    long lastInsertedContactId = command.LastInsertedId;

                    // Insertar los números de teléfono en la tabla de telefonos
                    foreach (string telefono in telefonos)
                    {
                        string telefonoQuery = "INSERT INTO telefono (numero, IdContacto) VALUES (@NumeroTelefono, @IdContacto)";
                        MySqlCommand telefonoCommand = new MySqlCommand(telefonoQuery, conexion);
                        telefonoCommand.Parameters.AddWithValue("@NumeroTelefono", telefono);
                        telefonoCommand.Parameters.AddWithValue("@IdContacto", lastInsertedContactId);
                        telefonoCommand.ExecuteNonQuery();
                    }

                    // Insertar los correos electrónicos en la tabla de correos electronicos
                    foreach (string email in emails)
                    {
                        string emailQuery = "INSERT INTO email (enderezo, idContacto) VALUES (@Email, @IdContacto)";
                        MySqlCommand emailCommand = new MySqlCommand(emailQuery, conexion);
                        emailCommand.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                        emailCommand.Parameters.AddWithValue("@IdContacto", lastInsertedContactId);
                        emailCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Los datos se han guardado en la base de datos correctamente.");

                    conexion.Close();
                    // Cerrar la ventana actual
                    this.Close();

                    MainWindow instance = new MainWindow();
                    instance.MostrarDatosEnDataGrid();
                    instance.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar los datos en la base de datos: " + ex.Message);
                }
            }
        }

    }





}

