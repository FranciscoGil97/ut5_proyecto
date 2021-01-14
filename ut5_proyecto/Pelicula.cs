﻿using System.ComponentModel;

namespace ut5_proyecto
{
    public class Pelicula : INotifyPropertyChanged
    {

        public enum Genero { Comedia, Drama, Acción, Terror, CienciaFicción };
        
        //El valor del enumerado de dificultad equivale a los puntos.
        public enum Dificultad { Facil = 10, Normal = 20, Dificil = 30 };

        private string titulo;
        private string pista;
        private string imagen;
        private Genero genero;
        private Dificultad dificultad;

        public Pelicula(string titulo, string pista, string imagen, Genero generoEnum, Dificultad dificultadEnum)
        {
            Titulo = titulo;
            Pista = pista;
            Imagen = imagen;
            _Genero = generoEnum;
            _Dificultad = dificultadEnum;
        }

        public Pelicula()
        {
            Titulo = "";
            Pista = "";
            Imagen = "";
            _Genero = Genero.Acción;
            _Dificultad = Dificultad.Facil;
        }

        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                NotifyPropertyChanged("Titulo");
            }
        }


        public string Pista
        {
            get { return pista; }
            set
            {
                pista = value;
                NotifyPropertyChanged("Pista");
            }
        }


        public string Imagen
        {
            get { return imagen; }
            set
            {
                imagen = value;
                NotifyPropertyChanged("Imagen");
            }
        }

        public Genero _Genero
        {
            get => genero;
            set
            {
                genero = value;
                NotifyPropertyChanged("_Genero");
            }
        }

        public Dificultad _Dificultad
        {
            get => dificultad;
            set
            {
                dificultad = value;
                NotifyPropertyChanged("_Dificultad");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
