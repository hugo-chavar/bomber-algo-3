using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;

namespace BombermanModel.Juego
{
    class CargadorDeMapa
    {
        public bool LeerMapa(string pathName)
        {
            try
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
            catch (Exception)
            {
                return false;
            }

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

            Casilla unaCasilla;
            switch (clase)
            {
                case "BloqueLadrillo":
                    unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(new Punto(x,y));
                    Juego.Instancia().Ambiente.AgregarCasilla(unaCasilla);
                    break;
                case "BloqueCemento":
                    unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(unaCasilla);
                    break;
                case "Pasillo":
                    unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(unaCasilla);
                    break;
                case "BloqueAcero":
                    unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(new Punto(x, y));
                    Juego.Instancia().Ambiente.AgregarCasilla(unaCasilla);
                    break;
                case "LosLopezReggae":
                    Juego.Instancia().AgregarEnemigo(new LosLopezReggae(new Punto(14, 1)));
                    break;

            }

        }
        public string ObtenerAtributo(string stringObjeto, string stringAtributo)
        {
            int pos = stringObjeto.IndexOf(stringAtributo+"\"");
            if (pos != -1)
            {
                int inicio = pos + (stringAtributo + "\"").Length;
                int fin = stringObjeto.IndexOf("\"", pos + 1);
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
                int fin = xmlTexto.IndexOf("/>");
                int len = fin + 1 - inicio;
                listaString.Add(xmlTexto.Substring(inicio, len));


                pos = xmlTexto.IndexOf(inicioObjeto,pos+1);
            }
            return listaString.ToArray();
        }
    }
}
