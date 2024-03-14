using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Lógica de interacción para EventosWindow.xaml
    /// </summary>
    public partial class EventosWindow : Window
    {
        public List<string> Eventos { get; set; } // Definición de la propiedad Eventos

        public EventosWindow(List<string> eventos)
        {
            InitializeComponent();
            Eventos = eventos; // Asignación de la lista de eventos al campo Eventos
            MostrarEventos(); // Método para mostrar los eventos en la ventana
        }

        private void MostrarEventos()
        {
            // Asignar la lista de eventos al ListBox
            EventosListBox.ItemsSource = Eventos;
        }

        private void EventosListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verificar si se ha seleccionado un evento
            if (EventosListBox.SelectedItem != null)
            {
                // Obtener el evento seleccionado
                string eventoSeleccionado = (string)EventosListBox.SelectedItem;

                // Separar el ID, la fecha y el contenido del evento
                string[] partes = eventoSeleccionado.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length >= 3)
                {
                    string idStr = partes[0].Trim(); // Obtener el ID del evento
                    string fecha = partes[1].Trim(); // Obtener la fecha del evento
                    string contenido = partes[2].Trim(); // Obtener el contenido del evento

                    // Convertir la cadena de fecha al formato DateTime
                    if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaEvento))
                    {
                        // La conversión fue exitosa, asignar el ID, la fecha y el contenido a la MainWindow
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        if (mainWindow != null)
                        {
                            mainWindow.IdEventoTextBox.Text = idStr;
                            mainWindow.datePicker.SelectedDate = fechaEvento;
                            mainWindow.ContenidoEventoTextBox.Text = contenido;
                        }
                    }
                    else
                    {
                        MessageBox.Show("La fecha del evento no tiene un formato válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



    }

}





