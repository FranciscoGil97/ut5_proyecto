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

        private ObservableCollection<Pelicula> peliculas;
        string[] dificultades = { "facil", "normal", "dificil" };
        string[] generos = { "comedia", "drama", "acción", "terror", "ciencia-ficción" };
        public MainWindow()
        {
            InitializeComponent();


            peliculas = new ObservableCollection<Pelicula>();
            peliculas.Add(new Pelicula("prudsfdsfsdfeba", "pista", @"https://www.wpf-tutorial.com/Images/ArticleImages/1/chapters/dialogs/openfiledialog_simple_app.png", "facil", "drama"));
            peliculas.Add(new Pelicula("prudsfdsfsdfeba", "pista", @"https://www.wpf-tutorial.com/Images/ArticleImages/1/chapters/dialogs/openfiledialog_simple_app.png", "normal", "acción"));

            listaPeliculas.DataContext = peliculas;
            generosComboBox.ItemsSource = generos;
        }

        private void ListaPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPeliculas.SelectedItem != null)
            {
                generosComboBox.DataContext = (Pelicula)listaPeliculas.SelectedItem;
                SeleccionaRaddioButton(((Pelicula)listaPeliculas.SelectedItem).Dificultad);
                camposGrid.DataContext = (Pelicula)listaPeliculas.SelectedItem;
            }
        }

        private void DificultadRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Tag.ToString() == dificultades[0])
                ((Pelicula)listaPeliculas.SelectedItem).Dificultad = dificultades[0];
            else
                ((Pelicula)listaPeliculas.SelectedItem).Dificultad =
                        rb.Tag.ToString() == dificultades[1] ? dificultades[1] : dificultades[2];

        }

        private void SeleccionaRaddioButton(string dificultad)
        {
            if (dificultad == dificultades[0])
                facilRadioButton.IsChecked = true;
            else if (dificultad == dificultades[1])
                normalRadioButton.IsChecked = true;
            else
                dificilRadioButton.IsChecked = true;
        }

        private void EliminarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            facilRadioButton.IsChecked =  false;
            normalRadioButton.IsChecked = false;
            dificilRadioButton.IsChecked =false;
            generosComboBox.DataContext = "";
            camposGrid.DataContext = "";
            peliculas.Remove((Pelicula)listaPeliculas.SelectedItem);
            listaPeliculas.SelectedItem = null;
        }


        //guardar archivo abriendo un dialogo
        //private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        //{
        ////    < StackPanel >
        ////< TextBox
        ////    Name = "txtEditor"
        ////    TextWrapping = "Wrap"
        ////    AcceptsReturn = "True"
        ////    ScrollViewer.VerticalScrollBarVisibility = "Auto" />
        ////< Button
        ////    Name = "btnSaveFile"
        ////    Click = "btnSaveFile_Click" > Save file </ Button >

        ////    < Button
        ////    Name = "btnOpenFile"
        ////    Click = "btnOpenFile_Click" > Open file </ Button >

        ////</ StackPanel >
        //        SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    if (saveFileDialog.ShowDialog() == true)
        //        File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
        //}

        ////abrir un archivos mediante un dialogo
        //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == true)
        //        txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        //}


        private void ExportarButton_Click(object sender, RoutedEventArgs e)
        {
            string personasJson = JsonConvert.SerializeObject(peliculas);
            File.WriteAllText("peliculas.json", personasJson);
        }

        private void ImportarButton_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader jsonStream = File.OpenText("peliculas.json"))
            {
                var json = jsonStream.ReadToEnd();
                List<Pelicula> peliculasJson = JsonConvert.DeserializeObject<List<Pelicula>>(json);

                foreach (Pelicula p in peliculasJson)
                    peliculas.Add(p);
            }
        }
    }
}
