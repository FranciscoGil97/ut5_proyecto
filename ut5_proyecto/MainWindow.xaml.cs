using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ut5_proyecto
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Pelicula> Peliculas;
        private const int NUMEROPELICULASJUEGO = 5;
        private List<int> indicePeliculasAleatoria = new List<int>();
        private List<int> indicePeliculaAcertada = new List<int>();
        private int numeroPeliculaJuego = 0;
        private int puntos = 0;
        private bool partidaIniciada = false;

        public MainWindow()
        {
            InitializeComponent();
            Peliculas = new ObservableCollection<Pelicula>();

            listaPeliculas.DataContext = Peliculas;
            generosComboBox.ItemsSource = Enum.GetValues(typeof(Pelicula.Genero));
            puntuacionTextBlock.DataContext = puntos;
        }

        private void ListaPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPeliculas.SelectedItem != null)
            {
                generosComboBox.DataContext = (Pelicula)listaPeliculas.SelectedItem;
                SeleccionaRaddioButton(((Pelicula)listaPeliculas.SelectedItem)._Dificultad);
                camposGrid.DataContext = (Pelicula)listaPeliculas.SelectedItem;
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
            if (dificultad == Pelicula.Dificultad.Facil)
                facilRadioButton.IsChecked = true;
            else if (dificultad == Pelicula.Dificultad.Normal)
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
            generosComboBox.DataContext = "";
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
                tituloPeliculaTextBox.Text = "";
                pistaPeliculaTextBox.Text = "";
                imagenPeliculaTextBox.Text = "";
            }
            else
                MessageBox.Show("Para añadir una película no debe haber ninguna pelicula seleccionada.");
        }

        private void NuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            if (Peliculas.Count >= 5)
            {
                indicePeliculasAleatoria = new List<int>();
                indicePeliculaAcertada = new List<int>();
                numeroPeliculaJuego = 0;
                partidaIniciada = true;
                Random seed = new Random();

                for (int i = 0; i < NUMEROPELICULASJUEGO; i++)
                {
                    int numRandom = seed.Next(0, Peliculas.Count);

                    if (!indicePeliculasAleatoria.Contains(numRandom))
                        indicePeliculasAleatoria.Add(numRandom);
                    else
                        i--;
                }
                numeroPeliculaTextBlock.Text = (numeroPeliculaJuego + 1) + "/" + NUMEROPELICULASJUEGO;
                pestanyJugarGrid.DataContext = Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]];
            }
            else
                MessageBox.Show("Deben haber al menos 5 películas para poder jugar.");
        }

        private void ValidarButton_Click(object sender, RoutedEventArgs e)
        {
            if (partidaIniciada && !indicePeliculaAcertada.Contains(indicePeliculasAleatoria[numeroPeliculaJuego]) && tituloPeliculaJuegoTextBox.Text == Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]].Titulo)
            {
                int dividePuntos = 1;
                if ((bool)pistaCheckBox.IsChecked)
                    dividePuntos = 2;

                puntos += (int)Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]]._Dificultad / dividePuntos;
                puntuacionTextBlock.DataContext = puntos;

                indicePeliculaAcertada.Add(indicePeliculasAleatoria[numeroPeliculaJuego]);
                tituloPeliculaJuegoTextBox.BorderBrush = Brushes.Black;
            }
            else
            {
                if (!indicePeliculaAcertada.Contains(indicePeliculasAleatoria[numeroPeliculaJuego]))
                    tituloPeliculaJuegoTextBox.BorderBrush = Brushes.Red;

            }
        }

        private void FlechaIzquierdaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (numeroPeliculaJuego > 0 && partidaIniciada)
            {
                --numeroPeliculaJuego;
                pestanyJugarGrid.DataContext = Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]];
                numeroPeliculaTextBlock.Text = (numeroPeliculaJuego + 1) + "/" + NUMEROPELICULASJUEGO;
                BorraPantallaJuego();
            }
        }

        private void FlechaDerechaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (numeroPeliculaJuego < NUMEROPELICULASJUEGO - 1 && partidaIniciada)
            {
                indicePeliculaAcertada.Add(indicePeliculasAleatoria[numeroPeliculaJuego]);
                ++numeroPeliculaJuego;
                pestanyJugarGrid.DataContext = Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]];
                numeroPeliculaTextBlock.Text = (numeroPeliculaJuego + 1) + "/" + NUMEROPELICULASJUEGO;
                BorraPantallaJuego();
            }
        }

        private void BorraPantallaJuego()
        {
            pistaCheckBox.IsChecked = false;
            pistaCheckBox.IsEnabled = true;
            pistaJuegoTextBlock.Width = 0;
            tituloPeliculaJuegoTextBox.Text = "";
            tituloPeliculaJuegoTextBox.BorderBrush = Brushes.Black;
        }

        private void PistaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)pistaCheckBox.IsChecked && partidaIniciada)
            {
                pistaJuegoTextBlock.Width = 450d;
                pistaJuegoTextBlock.Text = Peliculas[indicePeliculasAleatoria[numeroPeliculaJuego]].Pista;
                pistaCheckBox.IsEnabled = false;
            }
        }
    }
}
