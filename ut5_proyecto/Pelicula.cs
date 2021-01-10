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

        

        
        
        private string titulo;
        private string pista;
        private string imagen;
        private string dificultad;
        private string genero;

        public Pelicula(string titulo, string pista, string imagen, string dificultad, string genero)
        {
            Titulo = titulo;
            Pista = pista;
            Imagen = imagen;
            Dificultad = dificultad;
            Genero = genero;
        }

        public Pelicula()
        {
            Titulo = "";
            Pista = "";
            Imagen = "";
            Dificultad = "";
            Genero = "";
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


        public string Dificultad
        {
            get { return dificultad; }
            set 
            { 
                dificultad = value;
                NotifyPropertyChanged("Dificultad");
            }
        }


        public string Genero
        {
            get { return genero; }
            set 
            { 
                genero = value;
                NotifyPropertyChanged("Genero");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
