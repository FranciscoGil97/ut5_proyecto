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
namespace ut5_proyecto
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Pelicula> peliculas;

        public MainWindow()
        {
            InitializeComponent();

            peliculas = new ObservableCollection<Pelicula>();
            peliculas.Add(new Pelicula("prudsfdsfsdfeba", "pista", @"https://www.wpf-tutorial.com/Images/ArticleImages/1/chapters/dialogs/openfiledialog_simple_app.png", Pelicula.Dificultad.facil, Pelicula.Genero.accion));
            listaPeliculas.DataContext = peliculas;
            ObservableCollection<string> generos = new ObservableCollection<string>();
            generos.Add("Comedia");
            generos.Add("terror");
            generos.Add("Comedia2");
            generos.Add("terror2");

            generosComboBox.DataContext = generos;
            generosComboBox.SelectedItem = generos[0];
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
    }
}
