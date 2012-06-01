using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Casilla
    {
        protected Punto posicion;

        public abstract bool transitablePor(IMovible movil);


        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = Posicion; }
        }
    }
}
