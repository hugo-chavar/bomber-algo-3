using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Casilla:IDaniable
    {
        protected Punto posicion;

        public abstract bool transitablePor(IMovible movil);


        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = Posicion; }
        }

        public abstract void daniarConBombaToleTole();
        public abstract void daniarConBombaMolotov();
        //public abstract void daniarConProyectil();
    }
}
