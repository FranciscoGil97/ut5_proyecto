using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ut5_proyecto
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Pelicula> Peliculas;
        public MainWindow()
        {
            InitializeComponent();


            Peliculas = new ObservableCollection<Pelicula>();
            //Peliculas.Add(new Pelicula("prudsfdsfsdfeba", "pista", @"https://www.wpf-tutorial.com/Images/ArticleImages/1/chapters/dialogs/openfiledialog_simple_app.png",Pelicula.Genero.Drama,Pelicula.Dificultad.Facil));
            //Peliculas.Add(new Pelicula("prudsfdsfsdfeba", "pista", @"https://www.wpf-tutorial.com/Images/ArticleImages/1/chapters/dialogs/openfiledialog_simple_app.png", Pelicula.Genero.Acción, Pelicula.Dificultad.Normal));

            listaPeliculas.DataContext = Peliculas;
            generosComboBox.ItemsSource = Enum.GetValues(typeof(Pelicula.Genero));

            
        }

        private void ListaPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPeliculas.SelectedItem != null)
            {
                generosComboBox.DataContext = (Pelicula)listaPeliculas.SelectedItem;
                SeleccionaRaddioButton(((Pelicula)listaPeliculas.SelectedItem)._Dificultad);
                camposGrid.DataContext = (Pelicula)listaPeliculas.SelectedItem;
                pestanyJugarGrid.DataContext= (Pelicula)listaPeliculas.SelectedItem;
            }
        }

        private void DificultadRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (listaPeliculas.SelectedItem != null)
            {
                if ((bool)facilRadioButton.IsChecked)
                    ((Pelicula)listaPeliculas.SelectedItem)._Dificultad = Pelicula.Dificultad.Facil;
                else
                    ((Pelicula)listaPeliculas.SelectedItem)._Dificultad = (bool)normalRadioButton.IsChecked ?
                                                                Pelicula.Dificultad.Normal : Pelicula.Dificultad.Dificil;
            }

        }

        private void SeleccionaRaddioButton(Pelicula.Dificultad dificultad)
        {
            if(dificultad==Pelicula.Dificultad.Facil)
                facilRadioButton.IsChecked = true;
            else if(dificultad == Pelicula.Dificultad.Normal)
                normalRadioButton.IsChecked = true;
            else
                dificilRadioButton.IsChecked = true;
        }

        private void EliminarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            Peliculas.Remove((Pelicula)listaPeliculas.SelectedItem);
            QuitaSelecciones();
        }

        private void QuitarSeleccionButton_Click(object sender, RoutedEventArgs e)
        {
            QuitaSelecciones();
        }

        //abrir un archivos mediante un dialogo
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                imagenPeliculaTextBox.Text = openFileDialog.FileName;
        }

        private void ExportarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string urlArchivo = "";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if ((bool)saveFileDialog.ShowDialog())
                    urlArchivo = saveFileDialog.FileName;

                string personasJson = JsonConvert.SerializeObject(Peliculas);
                File.WriteAllText(urlArchivo, personasJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string urlArchivo = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Json files (*.json)|*.json";
                if ((bool)openFileDialog.ShowDialog())
                    urlArchivo = openFileDialog.FileName;
                using (StreamReader jsonStream = File.OpenText(urlArchivo))
                {
                    var json = jsonStream.ReadToEnd();
                    List<Pelicula> peliculasJson = JsonConvert.DeserializeObject<List<Pelicula>>(json);

                    foreach (Pelicula p in peliculasJson)
                        Peliculas.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void QuitaSelecciones()
        {
            facilRadioButton.IsChecked = false;
            normalRadioButton.IsChecked = false;
            dificilRadioButton.IsChecked = false;
            generosComboBox.DataContext = null;
            camposGrid.DataContext = "";
            listaPeliculas.SelectedItem = null;
        }

        private void AddPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            if (listaPeliculas.SelectedItem == null)
            {
                string titulo = tituloPeliculaTextBox.Text;
                string pista = pistaPeliculaTextBox.Text;
                string imagen = imagenPeliculaTextBox.Text;

                Pelicula.Genero genero = (Pelicula.Genero)generosComboBox.SelectedIndex;
                Pelicula.Dificultad dificultad;

                if ((bool)facilRadioButton.IsChecked)
                    dificultad = Pelicula.Dificultad.Facil;
                else
                    dificultad = (bool)normalRadioButton.IsChecked ? Pelicula.Dificultad.Normal : Pelicula.Dificultad.Dificil;


                Peliculas.Add(new Pelicula(titulo, pista, imagen, genero, dificultad));

                QuitaSelecciones();
            }
            else
                MessageBox.Show("Para añadir una película no debe haber ninguna pelicula seleccionada.");
        }



        private void NuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ValidarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FlechaIzquierdaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void FlechaDerechaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PistaCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
