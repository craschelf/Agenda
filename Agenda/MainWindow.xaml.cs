using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Agenda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Conexion mConexion;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            mConexion = new Conexion();
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;

            // Inicializar el DispatcherTimer con intervalo de 1 segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            // Iniciar el timer
            timer.Start();

            Calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Actualizar el TextBlock con la hora actual
            txtClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void EventosButton_Click(object sender, RoutedEventArgs e)
        {
            EventosButton.Background = Brushes.LightBlue;
            ContactosButton.Background = Brushes.White;
            NotasContent.Background = Brushes.White;

            ContactosContent.Visibility = Visibility.Collapsed;
            NotasContent.Visibility = Visibility.Collapsed;
            EventosContent.Visibility = Visibility.Visible;
        }

        private void ContactosButton_Click(object sender, RoutedEventArgs e)
        {
            ContactosButton.Background = Brushes.LightBlue;
            EventosButton.Background = Brushes.White;
            NotasButton.Background = Brushes.White;

            EventosContent.Visibility = Visibility.Collapsed;
            NotasContent.Visibility = Visibility.Collapsed;
            ContactosContent.Visibility = Visibility.Visible;


            MostrarDatosEnDataGrid();

        }

        public void MostrarDatosEnDataGrid()
        {
            // Mostramos los datos de las tres tablas combinadas
            MySqlDataReader sqlDataReader = null;
            string consulta = "SELECT " +
                                "contacto.*, " +
                                "email.enderezo, " +
                                "telefono.numero " +
                              "FROM " +
                                "contacto " +
                              "LEFT JOIN " +
                                "email ON contacto.idContacto = email.idContacto " +
                              "LEFT JOIN " +
                                "telefono ON contacto.idContacto = telefono.idContacto";

            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open(); // Abre la conexión si está cerrada
                }

                MySqlCommand sqlCommand = new MySqlCommand(consulta, conexion);
                sqlDataReader = sqlCommand.ExecuteReader();
            }

            DataTable dataTable = new DataTable();

            // Añadir columnas al DataTable
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Nombre");
            dataTable.Columns.Add("Apelido1");
            dataTable.Columns.Add("Apelido2");
            dataTable.Columns.Add("Comentario");
            dataTable.Columns.Add("Teléfono");
            dataTable.Columns.Add("Email");

            while (sqlDataReader.Read())
            {



                // Obtenemos datos de la database
                int id = sqlDataReader.GetInt32("idContacto");
                string nombre = sqlDataReader.GetString("nome");
                string apellido1 = sqlDataReader.IsDBNull("apelido1") ? "" : sqlDataReader.GetString("apelido1");
                string apellido2 = sqlDataReader.IsDBNull("apelido2") ? "" : sqlDataReader.GetString("apelido2");
                string comentario = sqlDataReader.IsDBNull("comentario") ? "" : sqlDataReader.GetString("comentario");
                string telefono = sqlDataReader.GetString("numero");
                string email = sqlDataReader.IsDBNull("enderezo") ? "" : sqlDataReader.GetString("enderezo");

                // Añadimos fila al DataTable
                dataTable.Rows.Add(id, nombre, apellido1, apellido2, comentario, telefono, email);
            }

            // Asignamos el DataTable al ItemsSource del DataGrid
            ContactosDataGrid.ItemsSource = dataTable.DefaultView;

            sqlDataReader.Close();
            conexion.Close();
        }


        private void NotasButtonClick(object sender, RoutedEventArgs e)
        {
            NotasButton.Background = Brushes.LightBlue;
            EventosButton.Background = Brushes.White;
            ContactosButton.Background = Brushes.White;

            EventosContent.Visibility = Visibility.Collapsed;
            ContactosContent.Visibility = Visibility.Collapsed;
            NotasContent.Visibility = Visibility.Visible;
        }

        private void NotasButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ContactosDataGrid.ItemsSource != null)
            {
                var dataView = ContactosDataGrid.ItemsSource as DataView;
                if (dataView != null)
                {
                    string filtro = SearchTextBox.Text.Trim();
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        // Filtrar por las columnas deseadas
                        dataView.RowFilter = string.Format("Nombre LIKE '%{0}%' OR Apelido1 LIKE '%{0}%' OR Apelido2 LIKE '%{0}%' OR Comentario LIKE '%{0}%' OR Teléfono LIKE '%{0}%' OR Email LIKE '%{0}%'", filtro);
                    }
                    else
                    {
                        // Limpiar el filtro
                        dataView.RowFilter = string.Empty;
                    }
                }
            }
        }

        private void AñadirContactoButton_Click(object sender, RoutedEventArgs e)
        {
            Formulario form = new Formulario();
            form.ShowDialog();
        }

        private void EditarContactoButton_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el DataGrid
            if (ContactosDataGrid.SelectedItem != null)
            {
                // Obtenemos la fila seleccionada
                DataRowView rowView = (DataRowView)ContactosDataGrid.SelectedItem;

                // Accedemos a los datos de la fila seleccionada
                string id = rowView["ID"].ToString();
                string nombre = rowView["Nombre"].ToString();
                string apellido1 = rowView["Apelido1"].ToString();
                string apellido2 = rowView["Apelido2"].ToString();
                string comentario = rowView["Comentario"].ToString();
                string telefono = rowView["Teléfono"].ToString();
                string email = rowView["Email"].ToString();


                //Abrimos el formulario y configuramos los valores de los TextBox
                FormularioEditar form = new FormularioEditar();
                form.IdTextBox.Text = id;
                form.NombreTextBox.Text = nombre;
                form.Apelido1TextBox.Text = apellido1;
                form.Apelido2TextBox.Text = apellido2;
                form.ComentarioTextBox.Text = comentario;
                form.TelefonoTextBox.Text = telefono;
                form.EmailTextBox.Text = email;
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BorrarContactoButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactosDataGrid.SelectedItem != null)
            {
                // Obtenemos la fila seleccionada
                DataRowView rowView = (DataRowView)ContactosDataGrid.SelectedItem;

                int id;
                // Accedemos a los datos de la fila seleccionada
                string idString = rowView["ID"].ToString();
                int.TryParse(idString, out id);

                string nombre = rowView["Nombre"].ToString();
                string apellido1 = rowView["Apelido1"].ToString();
                string apellido2 = rowView["Apelido2"].ToString();
                string comentario = rowView["Comentario"].ToString();
                string telefono = rowView["Teléfono"].ToString();
                string email = rowView["Email"].ToString();

                // Creamos un objeto Contacto con el ID obtenido
                Contacto contacto = new Contacto(id);

                // Llamamos al método para borrar el contacto
                BorrarContactoDeBaseDeDatos(contacto);

                MostrarDatosEnDataGrid();
            }
        }

        private void BorrarContactoDeBaseDeDatos(Contacto contacto)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                try
                {
                    // Eliminar contacto de la tabla de contactos
                    string queryDeleteContacto = "DELETE FROM contacto WHERE idContacto = @IdContacto";
                    MySqlCommand commandDeleteContacto = new MySqlCommand(queryDeleteContacto, conexion);
                    commandDeleteContacto.Parameters.AddWithValue("@IdContacto", contacto.Id);
                    commandDeleteContacto.ExecuteNonQuery();

                    // También puedes eliminar los números de teléfono y correos electrónicos asociados al contacto, si es necesario
                    // Eliminar los números de teléfono
                    string queryDeleteTelefono = "DELETE FROM telefono WHERE idContacto = @IdContacto";
                    MySqlCommand commandDeleteTelefono = new MySqlCommand(queryDeleteTelefono, conexion);
                    commandDeleteTelefono.Parameters.AddWithValue("@IdContacto", contacto.Id);
                    commandDeleteTelefono.ExecuteNonQuery();

                    // Eliminar los correos electrónicos
                    string queryDeleteEmail = "DELETE FROM email WHERE idContacto = @IdContacto";
                    MySqlCommand commandDeleteEmail = new MySqlCommand(queryDeleteEmail, conexion);
                    commandDeleteEmail.Parameters.AddWithValue("@IdContacto", contacto.Id);
                    commandDeleteEmail.ExecuteNonQuery();

                    MessageBox.Show("El contacto se ha borrado de la base de datos correctamente.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al borrar el contacto de la base de datos: " + ex.Message);
                }
            }
        }

        private void DuplicarContactoButton_Click(object sender, RoutedEventArgs e)
        {

            // Verificar si hay al menos una fila seleccionada en el DataGrid
            if (ContactosDataGrid.SelectedItem != null)
            {
                // Obtenemos la fila seleccionada
                DataRowView rowView = (DataRowView)ContactosDataGrid.SelectedItem;

                // Accedemos a los datos de la fila seleccionada
                string nombre = rowView["Nombre"].ToString();
                string apellido1 = rowView["Apelido1"].ToString();
                string apellido2 = rowView["Apelido2"].ToString();
                string comentario = rowView["Comentario"].ToString();
                string telefono = rowView["Teléfono"].ToString();
                string email = rowView["Email"].ToString();

                // Dividir los números de teléfono y las direcciones de correo electrónico en listas
                List<string> telefonos = new List<string>(telefono.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));
                List<string> emails = new List<string>(email.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));

                // Creamos un objeto Contacto con el ID obtenido
                Contacto contacto = new Contacto(nombre, apellido1, apellido2, comentario, telefonos, emails);
                DuplicarContacto(contacto);
                MostrarDatosEnDataGrid();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para duplicar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DuplicarContacto(Contacto contacto)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                try
                {
                    // Insertar los datos principales del contacto duplicado en la tabla de contactos
                    string query = "INSERT INTO contacto (nome, apelido1, apelido2, comentario) VALUES (@Nombre, @Apellido1, @Apellido2, @Comentario)";
                    MySqlCommand command = new MySqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                    command.Parameters.AddWithValue("@Apellido1", (object)contacto.Apellido1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Apellido2", (object)contacto.Apellido2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Comentario", (object)contacto.Comentario ?? DBNull.Value);
                    command.ExecuteNonQuery();

                    // Obtener el ID del contacto recién insertado
                    long lastInsertedContactId = command.LastInsertedId;

                    // Insertar los números de teléfono del contacto original en la tabla de teléfonos
                    foreach (string telefono in contacto.Telefonos)
                    {
                        string telefonoQuery = "INSERT INTO telefono (numero, IdContacto) VALUES (@NumeroTelefono, @IdContacto)";
                        MySqlCommand telefonoCommand = new MySqlCommand(telefonoQuery, conexion);
                        telefonoCommand.Parameters.AddWithValue("@NumeroTelefono", telefono);
                        telefonoCommand.Parameters.AddWithValue("@IdContacto", lastInsertedContactId);
                        telefonoCommand.ExecuteNonQuery();
                    }

                    // Insertar los correos electrónicos del contacto original en la tabla de correos electrónicos
                    foreach (string email in contacto.Emails)
                    {
                        string emailQuery = "INSERT INTO email (enderezo, idContacto) VALUES (@Email, @IdContacto)";
                        MySqlCommand emailCommand = new MySqlCommand(emailQuery, conexion);
                        emailCommand.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                        emailCommand.Parameters.AddWithValue("@IdContacto", lastInsertedContactId);
                        emailCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("El contacto se ha duplicado correctamente.");

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al duplicar el contacto en la base de datos: " + ex.Message);
                }
            }
        }

        private void AñadirEventoButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos de los TextBox
            DateTime fechaSeleccionada = datePicker.SelectedDate ?? DateTime.MinValue; // Obtener la fecha seleccionada del DatePicker
            string contenido = ContenidoEventoTextBox.Text;

            if (fechaSeleccionada == DateTime.MinValue || string.IsNullOrEmpty(contenido))
            {
                MessageBox.Show("Se debe agregar tanto la fecha como el contenido del evento");
                return; // Salir del método si el nombre está vacío
            }
            Evento evento = new Evento(fechaSeleccionada, contenido);
            InsertarEvento(evento);

        }

        private void InsertarEvento(Evento evento)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open(); // Abre la conexión si está cerrada
                }
                try
                {
                    // Preparar la consulta SQL para insertar el evento en la base de datos
                    string query = "INSERT INTO eventos.evento (fecha, contenido) VALUES (@fecha, @contenido)";

                    // Crear y configurar un comando SQL para ejecutar la consulta
                    using (MySqlCommand command = new MySqlCommand(query, conexion))
                    {
                        // Agregar los parámetros de la fecha y el contenido al comando SQL
                        command.Parameters.AddWithValue("@fecha", evento.Fecha);
                        command.Parameters.AddWithValue("@contenido", evento.Contenido);


                        // Ejecutar la consulta SQL
                        int rowsAffected = command.ExecuteNonQuery();

                        // Verificar si se insertaron filas correctamente
                        if (rowsAffected > 0)
                        {
                            // El evento se insertó correctamente en la base de datos
                            Console.WriteLine("Evento insertado correctamente.");
                        }
                        else
                        {
                            // Ocurrió un problema al insertar el evento en la base de datos
                            Console.WriteLine("Error al insertar el evento.");
                        }
                    }

                    conexion.Close(); // Cerrar la conexión
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar el evento en la base de datos: " + ex.Message);
                }
            }
        }


        private void EditarEventoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BorrarEventoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private DateTime? ultimaFechaSeleccionada = null;
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener la fecha seleccionada actualmente
            DateTime? nuevaFechaSeleccionada = Calendar.SelectedDate;

            // Verificar si la fecha seleccionada ha cambiado desde la última vez
            if (nuevaFechaSeleccionada != ultimaFechaSeleccionada)
            {
                // La fecha seleccionada ha cambiado, así que mostramos los eventos
                MostrarEventosParaFechaSeleccionada(nuevaFechaSeleccionada);

                // Actualizar la última fecha seleccionada
                ultimaFechaSeleccionada = nuevaFechaSeleccionada;
            }
        }

        private void MostrarEventosParaFechaSeleccionada(DateTime? fechaSeleccionada)
        {
            MySqlConnection conexion = mConexion.GetConexion(); // Obtener la conexión

            if (conexion != null)
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open(); // Abre la conexión si está cerrada
                }
                try
                {
                    // Convierte la fecha seleccionada al formato adecuado para la base de datos
                    string fecha = fechaSeleccionada.Value.ToString("yyyy-MM-dd");

                    string query = $"SELECT * FROM eventos.evento WHERE Fecha = '{fecha}'";
                    MySqlCommand command = new MySqlCommand(query, conexion);
                    MySqlDataReader reader = command.ExecuteReader();

                    // Lee los resultados de la consulta y muestra los eventos en una MessageBox
                    StringBuilder eventosBuilder = new StringBuilder();
                    while (reader.Read())
                    {
                        // Construye una cadena que contiene todos los datos de la fila
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            eventosBuilder.Append(reader.GetValue(i));
                            eventosBuilder.Append(" ");
                        }
                        eventosBuilder.AppendLine();
                    }
                    reader.Close();

                    string eventos = eventosBuilder.ToString().Trim();

                    if (!string.IsNullOrEmpty(eventos))
                    {
                        MessageBox.Show($"Eventos para {fechaSeleccionada.Value.ToShortDateString()}:\n{eventos}", "Eventos", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"No hay eventos para {fechaSeleccionada.Value.ToShortDateString()}", "Eventos", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al consultar la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}









