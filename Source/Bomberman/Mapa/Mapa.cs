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

        public Mapa(int tamanioHorizontal, int tamanioVertical)
        {
            this.dimensionHorizontal = tamanioHorizontal;
            this.dimensionVertical = tamanioVertical;
            tablero = new Casilla.Casilla[this.dimensionHorizontal, this.dimensionVertical];

        }

        public void agregarCasilla(Casilla.Casilla unaCasilla)
        {
            this.tablero[unaCasilla.Posicion.X, unaCasilla.Posicion.Y] = unaCasilla;
        }
    }
}
