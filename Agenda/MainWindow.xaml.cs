using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agenda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Conexion mConexion;

        public MainWindow()
        {
            InitializeComponent();
            mConexion = new Conexion();
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
                string telefono = sqlDataReader.IsDBNull("numero") ? "" : sqlDataReader.GetString("numero"); 
                string email = sqlDataReader.IsDBNull("enderezo") ? "" : sqlDataReader.GetString("enderezo"); 

                // Añadimos fila al DataTable
                dataTable.Rows.Add(id, nombre, apellido1, apellido2, comentario, telefono, email);
            }

            // Asignamos el DataTable al ItemsSource del DataGrid
            ContactosDataGrid.ItemsSource = dataTable.DefaultView;

            sqlDataReader.Close();
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
    }
}
