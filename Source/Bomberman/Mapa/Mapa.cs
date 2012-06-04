using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;

namespace Bomberman.Mapa
{
    public class Mapa : ITransitable
    {
        private Dictionary<Punto, Casilla.Casilla> tablero;
        private int dimensionHorizontal;
        private int dimensionVertical;

        public Dictionary<Punto, Casilla.Casilla> Tablero
        {
            get { return this.tablero; }
            set { this.tablero = value; }
        }

        public int DimensionHorizontal
        {
            get { return this.dimensionHorizontal; }
        }

        public int DimensionVertical
        {
            get { return this.dimensionVertical; }
        }

        public Mapa(int tamanioHorizontal, int tamanioVertical)
        {
            this.dimensionHorizontal = tamanioHorizontal;
            this.dimensionVertical = tamanioVertical;
            this.tablero = new Dictionary<Punto, Casilla.Casilla>();

        }

        public void agregarCasilla(Casilla.Casilla unaCasilla)
        {
            if (unaCasilla == null )
            {
                throw new NoExisteCasillaException();
            }
            if (unaCasilla.Posicion == null)
            {
                throw new PosicionNulaException();
            }
            if (!PosicionDentroRango(unaCasilla.Posicion))
            {
                throw new PuntoFueraDeRangoEnMapaException();
            }
            this.tablero.Add(unaCasilla.Posicion, unaCasilla);
        }

        private bool PosicionDentroRango(Punto punto)
        {
            return (punto.X < this.DimensionHorizontal && punto.Y < this.DimensionVertical);
        }

        public bool ExisteCasillaEnPosicion(Punto pos)
        {
            if (pos == null)
                throw new PosicionNulaException();
            return this.tablero.ContainsKey(pos);
        }

        public Casilla.Casilla ObtenerCasilla(Punto pos)
        {

            Casilla.Casilla unaCasilla;
            if (this.ExisteCasillaEnPosicion(pos))
            {
                unaCasilla = this.tablero[pos];
            }
            else
            {
                throw new NoExisteCasillaException();
            }
            return unaCasilla;

        }

        public bool PermitidoMoverHaciaArribaA(Personaje.IMovible movil)
        {
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(movil.Posicion);
            //Punto posicionSuperior = movil.Posicion.PosicionSuperior();



            return false;
        }

        public bool PermitidoMoverHaciaAbajoA(Personaje.IMovible movil)
        {
            return false;
        }

        public bool PermitidoMoverHaciaIzquierdaA(Personaje.IMovible movil)
        {
            return false;
        }

        public bool PermitidoMoverHaciaDerechaA(Personaje.IMovible movil)
        {
            return false;
        }

    }
}
