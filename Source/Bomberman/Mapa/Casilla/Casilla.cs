using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;
using Bomberman;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Casilla:IDaniable
    {
        protected Punto posicion;
        protected Articulo.Articulo articuloContenido;
        protected List<Personaje.Personaje> personajesEnCasilla;

        public abstract bool TransitablePor(IMovible movil);

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = Posicion; }
        }

        public Articulo.Articulo ArticuloContenido
        {
            get { return this.articuloContenido; }
            set { this.articuloContenido = ArticuloContenido; }
        }



        public abstract void DaniarConBombaToleTole();
        public abstract void DaniarConBombaMolotov();
        public abstract void DaniarConProyectil();
        //public abstract void daniarConProyectil();

        public void agregarArticulo(Articulo.Articulo unArticulo)
        {
            this.ArticuloContenido = unArticulo;
        }
    }
}
