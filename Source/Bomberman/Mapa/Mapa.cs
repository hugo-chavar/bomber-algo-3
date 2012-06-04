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
        //private Casilla.Casilla[,] tablero;
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
            //this.Tablero = (new Casilla.Casilla[this.dimensionHorizontal, this.dimensionVertical]);
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
            return (punto.X<this.DimensionHorizontal && punto.Y < this.DimensionVertical);
        }

        public bool existeCasillaEnPosicion(Punto pos)
        {
            if (pos == null)
                throw new PosicionNulaException();
            return this.tablero.ContainsKey(pos);
        }

        public Casilla.Casilla obtenerCasilla(Punto pos)
        {

            Casilla.Casilla unaCasilla;
            if (this.existeCasillaEnPosicion(pos))
            {
                unaCasilla = this.tablero[pos];
            }
            else
            {
                throw new NoExisteCasillaException();
            }
            return unaCasilla;

        }
    }
}
