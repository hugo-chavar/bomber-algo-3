using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;
using Bomberman;

namespace Bomberman.Mapa.Casilla
{
    public class Casilla //: IDaniable
    {
        private Punto posicion;
        private Articulo.Articulo articuloContenido;
        private List<IMovible> transitandoEnCasilla;
        //defino el patron State para determinar si hay un obstaculo o la casilla esta libre
        private Obstaculo estado;

        public Casilla(Punto pos)
        {
            // TODO: Complete member initialization
            this.posicion = pos;
            transitandoEnCasilla = new List<IMovible>();
        }

        public List<IMovible> TransitandoEnCasilla
        {
            get { return this.transitandoEnCasilla; }
        }

        //metodo que utiliza el patron State
        public bool PermiteTransitarUn(IMovible movil)
        {
            return this.Estado.TransitablePor(movil);
        }

        public void Transitar(IMovible movil)
        {
            this.transitandoEnCasilla.Add(movil);
            ((IPosicionable)movil).Posicion = this.Posicion; // ESTE CASTEO QUEDA HORRIBLE!
        }

        public void Dejar(IMovible movil)
        {
            this.transitandoEnCasilla.Remove(movil);
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }


        public Obstaculo Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }

        public Articulo.Articulo ArticuloContenido
        {
            get { return this.articuloContenido; }
            set { this.articuloContenido = value; }
        }

        public void agregarArticulo(Articulo.Articulo unArticulo)
        {
            if ((this.estado == null) | (this.estado.PuedeContenerArticulos()))   
            {
            this.ArticuloContenido = unArticulo;
            }

        }

        //public abstract void DaniarConBombaToleTole();
        //public abstract void DaniarConBombaMolotov();
        //public abstract void DaniarConProyectil();

       /* public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || this.GetType() != obj.GetType()) return false;
            Punto p = (Punto)obj;
            return (x == p.x) && (y == p.y);
        }*/

    }
}
