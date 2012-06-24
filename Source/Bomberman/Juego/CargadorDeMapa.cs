using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Articulo;
using BombermanModel.Personaje;

namespace BombermanModel.Juego
{
    class CargadorDeMapa
    {
        public bool LeerMapa(string pathName)
        {
            //try
            {
                StreamReader lector = new StreamReader(pathName);

                //asigno el texto xml a una variable
                string xmlTexto = lector.ReadToEnd();

                //separar todos los objetos
                string[] todosLosObjetos = SepararObjetos(xmlTexto);

                //crear los objetos a partir del array
                CrearLosObjetos(todosLosObjetos);

                return true;
            }
            //catch (Exception)
            //{
              // return false;
            //}

        }
        public void CrearLosObjetos(string[] arrayString)
        {
            foreach (string s in arrayString)
            {
                CrearObjeto(s);
            }

        }

        public void CrearObjeto(string s)
        {
            string clase = ObtenerAtributo(s,"class");
            int x = Convert.ToInt32(ObtenerAtributo(s, "x")); 
            int y = Convert.ToInt32(ObtenerAtributo(s, "y"));

            switch (clase)
            {
                case "BloqueLadrillo":
                    Casilla ladrillo = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(ladrillo);
                    break;
                case "BloqueCemento":
                    Casilla cemento = FabricaDeCasillas.FabricarCasillaConBloqueCemento(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(cemento);
                    break;
                case "Pasillo":
                    Casilla pasillo = FabricaDeCasillas.FabricarPasillo(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(pasillo);
                    break;
                case "BloqueAcero":
                    Casilla acero = FabricaDeCasillas.FabricarCasillaConBloqueAcero(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(acero);
                    break;
                case "Cecilio":
                    Personaje.Personaje cecilio = new Personaje.Cecilio(new Punto(x, y));
                    Juego.Instancia().AgregarEnemigo(cecilio);
                    break;
                case "LosLopezReggaeAlado":
                    Personaje.Personaje alado = new Personaje.LosLopezReggaeAlado(new Punto(x, y));
                    Juego.Instancia().AgregarEnemigo(alado);
                    break;
                case "LosLopezReggae":
                    Personaje.Personaje lopez = new Personaje.LosLopezReggae(new Punto(x, y));
                    Juego.Instancia().AgregarEnemigo(lopez);
                    break;
                case "Chala":
                    Articulo.Articulo chala = new Articulo.Chala();
                    Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(x, y)).agregarArticulo(chala);
                    break;
                case "ArticuloBombaToleTole":
                    Articulo.Articulo toleTole = new Articulo.ArticuloBombaToleTole();
                    Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(x, y)).agregarArticulo(toleTole);
                    break;
                case "Timer":
                    Articulo.Articulo timer = new Articulo.Timer();
                    Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(x, y)).agregarArticulo(timer);
                    break;
                case "Salida":
                    Articulo.Salida salida = new Articulo.Salida();
                    Juego.Instancia().Salida = salida;
                    Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(x, y)).agregarArticulo(salida);
                    break;
            }

        }
        public string ObtenerAtributo(string stringObjeto, string stringAtributo)
        {
            int pos = stringObjeto.IndexOf(stringAtributo);
            if (pos != -1)
            {
                int inicio = stringObjeto.IndexOf("\"", pos + (stringAtributo).Length) + 1;
                int fin = stringObjeto.IndexOf("\"", inicio + 1);
                int len = fin - inicio; //+ 1
                return stringObjeto.Substring(inicio, len);
            }
            else
            {
                return "";
            }
        }

        public string[] SepararObjetos(string xmlTexto)
        {
            List<string> listaString = new List<string>();
            //Eliminar cosas innecesarias
            xmlTexto = xmlTexto.Replace("\t", "");
            xmlTexto = xmlTexto.Replace("\n", "");
            xmlTexto = xmlTexto.Replace("<Objects>", "");
            xmlTexto = xmlTexto.Replace("</Objects>", "");

            string inicioObjeto = "<obj ";
            int pos = xmlTexto.IndexOf(inicioObjeto);
            if (pos != -1)
            {
                xmlTexto = xmlTexto.Remove(pos,inicioObjeto.Length);
            }

            while (pos != -1)
            {
                int inicio = pos;
                int fin = xmlTexto.IndexOf("/>", pos + 1);
                int len = fin + 1 - inicio;
                listaString.Add(xmlTexto.Substring(inicio, len));


                pos = xmlTexto.IndexOf(inicioObjeto,pos+1);
            }
            return listaString.ToArray();
        }
    }
}
