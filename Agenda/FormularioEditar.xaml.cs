using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para FormularioEditar.xaml
    /// </summary>
    public partial class FormularioEditar : Window
    {
        private Conexion mConexion;
        public FormularioEditar()
        {
            InitializeComponent();
            mConexion = new Conexion();
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
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

            EditarDatosEnBaseDeDatos(nombre, apellido1, apellido2, comentario, telefonos, emails);
        }

        private void EditarDatosEnBaseDeDatos(string nombre, string apellido1, string apellido2, string comentario, List<string> telefonos, List<string> emails)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                try
                {
                    // Verificar si hay al menos una fila seleccionada en el DataGrid
                    if (ContactosDataGrid.SelectedItem != null)
                    {
                        // Obtenemos la fila seleccionada
                        DataRowView rowView = (DataRowView)ContactosDataGrid.SelectedItem;

                        // Accedemos al ID del contacto seleccionado
                        int idContacto = (int)rowView["ID"];

                        // Actualizar los datos principales en la tabla de contactos
                        string query = "UPDATE contacto SET nome = @Nombre, apelido1 = @Apellido1, apelido2 = @Apellido2, comentario = @Comentario WHERE idContacto = @IdContacto";
                        MySqlCommand command = new MySqlCommand(query, conexion);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido1", (object)apellido1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Apellido2", (object)apellido2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Comentario", (object)comentario ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IdContacto", idContacto);
                        command.ExecuteNonQuery();

                        // Eliminar los números de teléfono y correos electrónicos existentes para este contacto
                        string queryDeleteTelefono = "DELETE FROM telefono WHERE idContacto = @IdContacto";
                        MySqlCommand commandDeleteTelefono = new MySqlCommand(queryDeleteTelefono, conexion);
                        commandDeleteTelefono.Parameters.AddWithValue("@IdContacto", idContacto);
                        commandDeleteTelefono.ExecuteNonQuery();

                        string queryDeleteEmail = "DELETE FROM email WHERE idContacto = @IdContacto";
                        MySqlCommand commandDeleteEmail = new MySqlCommand(queryDeleteEmail, conexion);
                        commandDeleteEmail.Parameters.AddWithValue("@IdContacto", idContacto);
                        commandDeleteEmail.ExecuteNonQuery();

                        // Insertar los nuevos números de teléfono en la tabla de telefonos
                        foreach (string telefono in telefonos)
                        {
                            string telefonoQuery = "INSERT INTO telefono (numero, IdContacto) VALUES (@NumeroTelefono, @IdContacto)";
                            MySqlCommand telefonoCommand = new MySqlCommand(telefonoQuery, conexion);
                            telefonoCommand.Parameters.AddWithValue("@NumeroTelefono", telefono);
                            telefonoCommand.Parameters.AddWithValue("@IdContacto", idContacto);
                            telefonoCommand.ExecuteNonQuery();
                        }

                        // Insertar los nuevos correos electrónicos en la tabla de correos electronicos
                        foreach (string email in emails)
                        {
                            string emailQuery = "INSERT INTO email (enderezo, idContacto) VALUES (@Email, @IdContacto)";
                            MySqlCommand emailCommand = new MySqlCommand(emailQuery, conexion);
                            emailCommand.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                            emailCommand.Parameters.AddWithValue("@IdContacto", idContacto);
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
                    else
                    {
                        MessageBox.Show("Por favor, seleccione una fila para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar los datos en la base de datos: " + ex.Message);
                }
            }
        }

    }

}

