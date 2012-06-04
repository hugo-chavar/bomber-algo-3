using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;

namespace Bomberman.Juego
{
    public class Juego
    {
        private int cantDeVidas;
        private bool juegoPausado;
        private Personaje.Personaje protagonista;
        private Mapa.Mapa ambiente;
        //declaracion del Singleton
        private static Juego instanciaDeJuego;
        //Constantes
        private const int VIDAS = 3;
        private const int ANCHOMAPA = 10;
        private const int ALTOMAPA = 20;

        //propiedades
        public int CantDeVidas
        {
            get { return cantDeVidas; }
            set { this.cantDeVidas = value; }
        }

        public bool JuegoPausado
        {
            get { return juegoPausado; }
            set { this.juegoPausado = value; }
        }

        public Personaje.Personaje Protagonista
        {
            get { return protagonista; }
            set { this.protagonista = value; }
        }

        public Mapa.Mapa Ambiente
        {
            get { return ambiente; }
            set { this.ambiente = value; }
        }

        //constructor
        public Juego()
        {
            Punto unPunto = new Punto ( 0 , 0 );
            this.Protagonista = new Personaje.Bombita(unPunto);
            this.JuegoPausado = false;
            this.CantDeVidas = VIDAS;
            this.Ambiente = new Mapa.Mapa(ANCHOMAPA,ALTOMAPA);

        }
        
        //instanciacion del Singleton
        public static Juego Instancia()
        {
            if (instanciaDeJuego == null)
            {
                instanciaDeJuego = new Juego();
                //aca van las inicializaciones que van por
                //afuera del constructor
            }
            return instanciaDeJuego;
        }


    }
}
