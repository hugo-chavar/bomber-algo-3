using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Mapa
{
    public class Mapa : ITransitable
    {
        private Casilla.Casilla[,] tablero;
        private int dimensionHorizontal;
        private int dimensionVertical;

        public Casilla.Casilla[,] Tablero
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
            this.Tablero = (new Casilla.Casilla[this.dimensionHorizontal, this.dimensionVertical]);

        }

        public void agregarCasilla(Casilla.Casilla unaCasilla)
        {
            (tablero[(unaCasilla.Posicion.X), (unaCasilla.Posicion.Y)]) = (unaCasilla);
        }
    }
}
