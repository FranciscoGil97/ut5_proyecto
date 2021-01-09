using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ut5_proyecto
{
    public class Pelicula : INotifyPropertyChanged
    {

        public enum Dificultad { facil, normal, dificil }

        public enum Genero { comedia, drama, accion, terror, cienciaFiccion }
        
        private string titulo;
        private string pista;
        private string imagen;
        private Dificultad dificultad;
        private Genero genero;

        public Pelicula(string titulo, string pista, string imagen, Dificultad dificultad, Genero genero)
        {
            Titulo = titulo;
            Pista = pista;
            Imagen = imagen;
            _Dificultad = dificultad;
            _Genero = genero;
        }

        public Pelicula()
        {
            Titulo = "";
            Pista = "";
            Imagen = "";
            _Dificultad = Dificultad.facil;
            _Genero = Genero.accion;
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


        public Dificultad _Dificultad
        {
            get { return dificultad; }
            set 
            { 
                dificultad = value;
                NotifyPropertyChanged("_Dificultad");
            }
        }


        public Genero _Genero
        {
            get { return genero; }
            set 
            { 
                genero = value;
                NotifyPropertyChanged("_Genero");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
