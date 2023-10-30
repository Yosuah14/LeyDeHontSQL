using LeyDeHont.Domain;
using LeyDeHont.Persistence.Manages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeyDeHont
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatosPartido p;
        public MainWindow()
        {
            InitializeComponent();
            PartiesManage pm = new PartiesManage();
            TextBox2.TextChanged += changeNullVotes;
            TextBox3.TextChanged += changeNullVotes;
            p = new DatosPartido();
            dgvPeople.ItemsSource = p.getListParties();
        }
        private void btnExample_Click(object sender, RoutedEventArgs e)
        {

            if (p.pm.listParties ==null)
            {
                p = new DatosPartido();
                dgvPeople.ItemsSource = p.getListParties();
            }

            if (string.IsNullOrEmpty(acronimo.Text) || string.IsNullOrEmpty(nPartido.Text) || string.IsNullOrEmpty(txtPresidente.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de agregar una nueva entrada.");
                }
                else if (dgvPeople.Items.Count > 10)
                {
                    MessageBox.Show("No se pueden agregar más de 10 partidos.");
                }
                else
                {
                    if (dgvPeople.Items.Count > 9)
                    {
                        PreviousData pd = new PreviousData();
                        int text1, text2, text3;

                        if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox3.Text) &&
                            int.TryParse(TextBox1.Text, out text1) && int.TryParse(TextBox2.Text, out text2) && int.TryParse(TextBox3.Text, out text3))
                        {
                            int validVotes = PreviousData.calculatevalidvotes(text1, text2, text3);
                            pd = new PreviousData(text1, text2, text3, validVotes);
                        }
                        else
                        {
                            // Manejo de error si los valores no son válidos
                            return;
                        }
                        List<DatosPartido> listaDePartidosF = p.pm.listParties;
                        listaDePartidosF = PartidosFactory.inicialiteParties(pd, listaDePartidosF);
                        simulation.IsEnabled = true;
                        dgvParties.ItemsSource = listaDePartidosF;
                        dgvParties.Items.Refresh();
                    }
                    else
                    {
                        p.addParties(acronimo.Text, nPartido.Text, txtPresidente.Text);
                        dgvPeople.Items.Refresh();
                    }
                    // Todos los campos están llenos y no se ha alcanzado el límite de 10 partidos, puedes agregar la entrada
                

            }
           
        }
        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            List<DatosPartido> partidosABorrar = dgvPeople.SelectedItems.Cast<DatosPartido>().ToList();
            foreach (DatosPartido partido in partidosABorrar)
            {
                p.removeParties(partido);
                p.pm.listParties.Remove(partido);
            }
            dgvPeople.Items.Refresh();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int text1=0, text2=0, text3;
            // Verificar si todas las cajas de texto están llenas y si contienen números válidos
            if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox3.Text) &&
                int.TryParse(TextBox1.Text, out text1) && int.TryParse(TextBox2.Text, out text2) && int.TryParse(TextBox3.Text, out text3)&&text1 > text2)
            {
                PreviousData p;
                // Todos los TextBox contienen números válidos
                int validvotes= PreviousData.calculatevalidvotes(text1, text2, text3);
                 p = new PreviousData(text1, text2, text3,validvotes);
                // Habilitar las pestañas "PARTIES MANAGEMENT" y "SIMULATION"
                managment.IsEnabled = true;
                managment.IsSelected = true;
            }
            else
            {
                if (string.IsNullOrEmpty(TextBox2.Text) && string.IsNullOrEmpty(TextBox3.Text))
                {
                    MessageBox.Show("Uno o más de los valores en las cajas de texto no son números enteros válidos o están vacíos.");
                    managment.IsEnabled = false;
                    simulation.IsEnabled = false;
                   
                }
                if (text1 < text2)
                {
                    MessageBox.Show("No puede ser el numero de  ABSTENTIONS VOTES mayor que POPULATION");
                    managment.IsEnabled = false;
                    simulation.IsEnabled = false;

                }
                else
                {
                    MessageBox.Show("No se puede introducir letras es un numero");

                }
             
            }
            // Establecer el foco en la pestaña "PARTIES MANAGEMENT"
        }

        private void dgvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvPeople.SelectedItem != null)
            {
                // Seleccionaron una fila, muestra el botón
                btnBorrado.Visibility = Visibility.Visible;
            }
            else
            {
                // No hay fila seleccionada, oculta el botón
                btnBorrado.Visibility = Visibility.Collapsed;
            }
        }
        private void changeNullVotes(object sender, RoutedEventArgs e)
        {
            int text1;
            int text2;
            double text3;
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                int.TryParse(TextBox1.Text, out text1);
                int.TryParse(TextBox2.Text, out text2);
                double.TryParse(TextBox3.Text, out text3);
                text3 = PreviousData.CalculateNullVotes(text1, text2);
                TextBox3.Text = text3.ToString();

            }
            else
            {
                // Mantener las pestañas deshabilitadas si alguna caja de texto no está llena
                managment.IsEnabled = false;
                simulation.IsEnabled = false;
                MessageBox.Show("Los valores en las cajas de texto no son números enteros válidos.");
            }

        }
        private void Simulate_Click(object sender, RoutedEventArgs e)
        {
            if (p.pm.listParties==null) {
                MessageBox.Show("Neceistas cambiar los datos de la simulacion para hacer otra");
                managment.Focus();
            }
            else
            {
                  PreviousData pd = new PreviousData(); // Creas una instancia de PreviousData

            // Obtiene los valores necesarios para inicializar PreviousData y luego la utilizas
            int text1, text2, text3;

            if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox3.Text) &&
                int.TryParse(TextBox1.Text, out text1) && int.TryParse(TextBox2.Text, out text2) && int.TryParse(TextBox3.Text, out text3))
            {
                int validVotes = PreviousData.calculatevalidvotes(text1, text2, text3);
                pd = new PreviousData(text1, text2, text3, validVotes);
            }
            else
            {
                // Manejo de error si los valores no son válidos
                return;
            }
      // Utilizas el método PartidosFactory.inicialiteParties con los datos requeridos
            List<DatosPartido> listaDePartidosIni = p.pm.listParties;
            listaDePartidosIni = PartidosFactory.inicialiteParties(pd, listaDePartidosIni);           
            listaDePartidosIni =DatosPartido.CalculateSeats(listaDePartidosIni);
            listaDePartidosIni[0].votes = DatosPartido.calculateVotesPartie(0.3524, pd);
            listaDePartidosIni[1].votes = DatosPartido.calculateVotesPartie(0.2475, pd);
            listaDePartidosIni[2].votes = DatosPartido.calculateVotesPartie(0.1575, pd);
            listaDePartidosIni[3].votes = DatosPartido.calculateVotesPartie(0.1425, pd);
            listaDePartidosIni[4].votes = DatosPartido.calculateVotesPartie(0.0375, pd);
            listaDePartidosIni[5].votes = DatosPartido.calculateVotesPartie(0.0325, pd);
                // Creando una nueva lista para combinar los datos
                // Asignando la nueva lista a la fuente de datos del DataGrid
            MessageBox.Show("Se ha reaizado la simulacion");
            dgvParties.ItemsSource = listaDePartidosIni;
            dgvParties.Items.Refresh();
            p.pm.listParties = null;
            dgvPeople.ItemsSource = null;
            

            }
          
        }

    }

}

